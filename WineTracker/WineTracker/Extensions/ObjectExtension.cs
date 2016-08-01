using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WineTracker.Extensions
{
    
    public static class ObjectExtension
    {
        public static IDictionary<string, string> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, string> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = (from x in source.GetType().GetRuntimeProperties() select x)
                .ToDictionary(x => x.Name,
                    x => (x.GetMethod.Invoke(source, null) == null ? "" : x.GetMethod.Invoke(source, null).ToString()));


            return dictionary;
        }
        
        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}
