using Ninject.Modules;
using OpenHab.Wpf.ViewModel.ViewModels;

namespace OpenHab.Wpf.ViewModel.Module
{
    public class ViewModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServerViewModel>().ToSelf().InSingletonScope();
        }
    }
}