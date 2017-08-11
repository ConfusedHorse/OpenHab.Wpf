using System.Text.RegularExpressions;

namespace OpenHab.Wpf.CrossCutting.Helpers
{
    public static class IpHelper
    {
        public static bool ValidateIPv4(string address)
        {
            return !string.IsNullOrWhiteSpace(address) && (address == "localhost" ||
                                                           Regex.Match(address,
                                                               @"^\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b$").Success);
        }
    }
}