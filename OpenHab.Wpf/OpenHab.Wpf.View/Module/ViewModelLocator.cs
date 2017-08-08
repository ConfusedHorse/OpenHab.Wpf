using System.ComponentModel;
using Ninject;
using OpenHab.Wpf.CrossCutting.Module;

namespace OpenHab.Wpf.View.Module
{
    public class ViewModelLocator
    {
        #region Singleton

        private static ViewModelLocator _instance;

        public static ViewModelLocator Instance
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return null;
                return _instance ?? (_instance = new ViewModelLocator());
            }
        }

        #endregion Singleton

        //public TODOViewModel TODOViewModel => NinjectKernel.StandardKernel.Get<TODOViewModel>();

        public static void Cleanup()
        {

        }
    }
}