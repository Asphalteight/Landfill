using System.Linq;

namespace Landfill.Common.Helpers
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str); 

        public static bool In(this object obj, params object[] values) => values.Contains(obj);
    }
}
