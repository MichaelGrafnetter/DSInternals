
using System;

namespace DSInternals.DataStore
{
    public static class DateTimeExtensions
    {
        public static long ToGeneralizedTime(this DateTime time)
        {
            return time.ToFileTime() / ADConstants.GeneralizedTimeCoefficient;
        }
        public static long ToTimeStamp(this DateTime time)
        {
            return time.ToFileTime();
        }
    }
}
