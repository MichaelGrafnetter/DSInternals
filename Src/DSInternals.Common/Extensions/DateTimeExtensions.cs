using System;

namespace DSInternals.Common
{
    /// <summary>
    /// Provides extension methods for DateTime objects to convert to and from Active Directory generalized time format.
    /// </summary>
    public static class DateTimeExtensions
    {
        private const int GeneralizedTimeCoefficient = 10000000;

        /// <summary>
        /// Converts a DateTime to Active Directory generalized time format.
        /// </summary>
        /// <param name="time">The DateTime to convert.</param>
        /// <returns>The generalized time representation as a long value.</returns>
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
