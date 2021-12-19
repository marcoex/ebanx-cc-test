using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace System
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Object value)
        { 
            var fieldInfo = value.GetType().GetField(value.ToString());
            var desc = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return desc?.Description ?? value.ToString();
        }
    }
}
