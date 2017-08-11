using System;
using System.Linq;
using OpenHAB.NetRestApi.Helpers;

namespace OpenHab.Wpf.CrossCutting.Helpers
{
    public static class StringExtension
    {
        public static bool Search(this string browseIn, string suchbegriff, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(suchbegriff))
            {
                return true;
            }
            if (string.IsNullOrEmpty(browseIn))
            {
                return false;
            }
            var splittedString = suchbegriff.Split(' ', ',').Where(s => s != string.Empty).ToArray();

            return splittedString.IsEmpty() || splittedString.All(s => browseIn.IndexOf(s, comparisonType) >= 0);
        }
    }
}