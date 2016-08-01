using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WineTracker.Extensions;
using Xamarin;

namespace WineTracker.Components
{
   
        public static class AsyncErrorHandler
        {
            public static void HandleInsightException<T>(Exception exception, T objectData)
            {
                var additionalInformation = objectData.ToDictionary<T>() as Dictionary<string, string>;
                Insights.Report(exception, additionalInformation);
                HandleException(exception);
            }

            public static void HandleInsightException<T>(Exception exception, Dictionary<string, string> additionalInformation, T objectData)
            {


                additionalInformation.Add("OBJECT_DATA_START", "++++++++++++++++++++++++++++++++++++++++++");
                additionalInformation.Add("OBJECT_DATA", JsonConvert.SerializeObject(objectData));
                additionalInformation.Add("OBJECT_DATA_END", "++++++++++++++++++++++++++++++++++++++++++++");
                Insights.Report(exception, additionalInformation);
                HandleException(exception);

            }
            public static void HandleInsightException(Exception exception, Dictionary<string, string> additionalInformation)
            {
                Insights.Report(exception, additionalInformation);
                HandleException(exception);
            }
            public static void HandleException(Exception exception)
            {
                Insights.Report(exception);

#if DEBUG
                //if (!(exception.Message.Contains("40300") || exception.Message.Contains("40397")))
                //	DependencyService.Get<ITelemetry>().ShowLog();
#endif

            }
        }
    
}
