using System;

namespace DSInternals.Common
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a value of an Active Directory attribute with Generalized-Time syntax to DateTime.
        /// </summary>
        /// <remarks>
        /// Active Directory attributes stored in this format include whenCreated, whenChanged, createTimeStamp, and modifyTimeStamp.
        /// </remarks>
        public static DateTime FromGeneralizedTime(this long timeStamp)
        {
            // Convert the timestamp from seconds to 100ns first
            return DateTime.FromFileTime(timeStamp * 10000000);
        }

        public static long ToGeneralizedTime(this DateTime time)
        {
            // Convert the resulting timestamp from 100ns to seconds.
            return time.ToFileTime() / 10000000;
        }
    }
}
