using System;
using System.Collections.Generic;
using System.Text;

namespace FourTwenty.IoT.Connect.Helpers
{
    public static class DateHelper
    {
        public static string GetTimeSince(this DateTime date)
        {
            var seconds = Math.Floor((DateTime.Now - date).TotalSeconds / 1000);

            var interval = seconds / 31536000;

            if (interval > 1)
            {
                return Math.Floor(interval) + " years";
            }
            interval = seconds / 2592000;
            if (interval > 1)
            {
                return Math.Floor(interval) + " months";
            }
            interval = seconds / 86400;
            if (interval > 1)
            {
                return Math.Floor(interval) + " days";
            }
            interval = seconds / 3600;
            if (interval > 1)
            {
                return Math.Floor(interval) + " hours";
            }
            interval = seconds / 60;
            if (interval > 1)
            {
                return Math.Floor(interval) + " minutes";
            }
            return Math.Floor(seconds) + " seconds";
        }

    }
}
