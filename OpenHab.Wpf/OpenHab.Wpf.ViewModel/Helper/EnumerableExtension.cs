using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OpenHab.Wpf.ViewModel.Helper
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self) action(item);
        }

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
        {
            return self == null || self.IsEmpty();
        }

        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            return !self.Any();
        }

        [DebuggerStepThrough]
        public static T[] EnumToArray<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}