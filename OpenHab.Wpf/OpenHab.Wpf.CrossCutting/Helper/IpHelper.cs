using System.Linq;

namespace OpenHab.Wpf.CrossCutting.Helper
{
    public static class IpHelper
    {
        public static bool ValidateIPv4(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            if (address == "localhost") return true;

            var splitValues = address.Split('.');
            return splitValues.Length == 4 && splitValues.All(r => byte.TryParse(r, out byte tempForParsing));
        }
    }
}