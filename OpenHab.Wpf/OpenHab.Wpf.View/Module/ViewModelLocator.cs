using System.ComponentModel;
using Ninject;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.ViewModels;

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

        public ServerViewModel ServerViewModel => NinjectKernel.StandardKernel.Get<ServerViewModel>();
        public SitemapViewModel SitemapViewModel => NinjectKernel.StandardKernel.Get<SitemapViewModel>();
        public ItemsViewModel ItemsViewModel => NinjectKernel.StandardKernel.Get<ItemsViewModel>();
        public ThingsViewModel ThingsViewModel => NinjectKernel.StandardKernel.Get<ThingsViewModel>();

        public static void Cleanup()
        {

        }
    }
}