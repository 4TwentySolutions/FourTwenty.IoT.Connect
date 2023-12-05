using System;

namespace FourTwenty.IoT.Connect.Helpers
{
    public static class DateHelper
    {
        public static string ElapsedTime(this DateTime dtEvent)
        {
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
            if (intSeconds == 0) return "0 seconds ago";

            return dtEvent.ToString("g");
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
