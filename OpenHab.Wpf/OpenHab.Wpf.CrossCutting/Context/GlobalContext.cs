using OpenHab.Wpf.CrossCutting.Properties;

namespace OpenHab.Wpf.CrossCutting.Context
{
    public class GlobalContext
    {
        public Settings Settings => Settings.Default;

        ~GlobalContext()
        {
            Settings.Save();
        }
    }
}