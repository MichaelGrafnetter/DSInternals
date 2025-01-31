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

        /// <summary>
        /// Converts a value of an Active Directory attribute with Generalized-Time syntax to DateTime.
        /// </summary>
        /// <remarks>
        /// Active Directory attributes stored in this format include whenCreated, whenChanged, createTimeStamp, and modifyTimeStamp.
        /// </remarks>
        public static DateTime FromGeneralizedTime(this long timestamp)
        {
            return DateTime.FromFileTime(timestamp * GeneralizedTimeCoefficient);
        }
    }
}
