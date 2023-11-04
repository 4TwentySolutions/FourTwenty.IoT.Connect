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

        public static string ElapsedTime(this DateTime dtEvent)
        {
            TimeSpan TS = DateTime.Now - dtEvent;
            var intYears = DateTime.Now.Year - dtEvent.Year;
            var intMonths = DateTime.Now.Month - dtEvent.Month;
            var intDays = DateTime.Now.Day - dtEvent.Day;
            var intHours = DateTime.Now.Hour - dtEvent.Hour;
            var intMinutes = DateTime.Now.Minute - dtEvent.Minute;
            var intSeconds = DateTime.Now.Second - dtEvent.Second;

            if (intYears > 0) return $"{intYears} {((intYears == 1) ? "year" : "years")} ago";
            if (intMonths > 0) return $"{intMonths} {((intMonths == 1) ? "month" : "months")} ago";
            if (intDays > 0) return $"{intDays} {((intDays == 1) ? "day" : "days")} ago";
            if (intHours > 0) return $"{intHours} {((intHours == 1) ? "hour" : "hours")} ago";
            if (intMinutes > 0) return $"{intMinutes} {((intMinutes == 1) ? "minute" : "minutes")} ago";
            if (intSeconds > 0) return $"{intSeconds} {((intSeconds == 1) ? "second" : "seconds")} ago";

            return $"{dtEvent.ToShortDateString()} {dtEvent.ToShortTimeString()} ago";
        }

        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        public static DateTime ResetTime(this DateTime dateTime)
        {
            return ChangeTime(dateTime, 0, 0, 0, 0);
        }
    }
}
