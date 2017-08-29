using System;

namespace OpenHab.Wpf.CrossCutting.Helpers
{
    public static class EnumHelper
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this string value)
        {
            return ParseEnum<T>(value);
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            return string.IsNullOrEmpty(value)
                ? defaultValue
                : (Enum.TryParse(value, true, out T result) ? result : defaultValue);
        }
    }
}