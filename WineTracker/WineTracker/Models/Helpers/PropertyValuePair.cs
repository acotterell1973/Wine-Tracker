﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineTracker.Models.Helpers
{
    public class PropertyValuePair
    {
        public PropertyValuePair(string key, object value)
        {
            Property = key;
            if (value != null)
                Value = value.ToString();
        }

        public PropertyValuePair()
        {
        }

        public string Property { get; set; }

        public string Value { get; set; }

        public int ValueToInt32()
        {
            if (string.IsNullOrEmpty(Value))
                return 0;
            int intValue;
            if (int.TryParse(Value, out intValue))
                return intValue;
            return 0;
        }
    }
}
