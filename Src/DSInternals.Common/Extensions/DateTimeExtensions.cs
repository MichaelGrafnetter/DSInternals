using System;

namespace DSInternals.Common
{
    public static class DateTimeExtensions
    {
        private const int GeneralizedTimeCoefficient = 10000000;

        public static long ToGeneralizedTime(this DateTime time)
        {
            return time.ToFileTime() / GeneralizedTimeCoefficient;
        }

        public static DateTime FromGeneralizedTime(this long timestamp)
        {
            return DateTime.FromFileTime(timestamp * GeneralizedTimeCoefficient);
        }

        public static long ToTimeStamp(this DateTime time)
        {
            return time.ToFileTime();
        }
    }
}
