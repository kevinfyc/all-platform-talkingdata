namespace TalkingDataGAWP.command
{
    using System;

    internal class DateTimeUtils
    {
        public static long getCurrentTime()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - DateTime.Parse("1970-1-1"));
            return (long) span.TotalMilliseconds;
        }
    }
}

