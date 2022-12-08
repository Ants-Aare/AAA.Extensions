using System;

namespace AAA.Utility.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Min(TimeSpan t1, TimeSpan t2)
        {
            return t1 < t2 ? t1 : t2;
        }

        public static TimeSpan Max(TimeSpan t1, TimeSpan t2)
        {
            return t1 < t2 ? t2 : t1;
        }

        public static string ToStringHMS(this TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm\:ss");
        }

        public static string ToStringMostRelvantTwoDHMS(this TimeSpan timeSpan)
        {
            if (timeSpan.Days > 0)
            {
                return $"{timeSpan:%d}d {timeSpan:hh}h";
            }

            if (timeSpan.Hours > 0)
            {
                return $"{timeSpan:hh}h {timeSpan:mm}m";
            }

            return $"{timeSpan:mm}m {timeSpan:ss}s";
        }
    }
}
