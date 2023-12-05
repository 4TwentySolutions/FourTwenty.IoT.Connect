using System;

namespace FourTwenty.IoT.Server.Extensions
{
    public static class GenericExtensions
    {
        public static string TruncateLongString(this string str, int maxLength) => str?[..Math.Min(str.Length, maxLength)];
    }
}
