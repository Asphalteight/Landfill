using System.ComponentModel;
using System;
using System.Linq;

namespace Landfill.Common.Helpers
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str); 

        public static bool In(this object obj, params object[] values) => values.Contains(obj);

        public static string GetDescription(this Enum value)
        {
            var fi = value?.GetType()?.GetField(value?.ToString());
            if (fi == null)
            {
                return string.Empty;
            }

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
