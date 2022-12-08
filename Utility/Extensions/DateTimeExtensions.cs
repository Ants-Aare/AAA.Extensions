using System;

namespace AAA.Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Epoch => new (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static int DaysInMonth(this DateTime dateTime)
            => DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        /// <summary>
        /// The unix time stamp is a way to track time as a running total of seconds. This count starts at the Unix
        /// Epoch on January 1st, 1970 at UTC. Therefore, the unix time stamp is merely the number of seconds between
        /// a particular date and the Unix Epoch.
        /// https://www.unixtimestamp.com/index.php
        /// </summary>
        public static TimeSpan TimeSpanSinceEpoch(this DateTime dateTime)
            => dateTime - Epoch;

        public static bool IsBetween(this DateTime dateTime, DateTime startDateTime, DateTime endDateTime)
        {
            return dateTime >= startDateTime && dateTime <= endDateTime;
        }
    }
}
