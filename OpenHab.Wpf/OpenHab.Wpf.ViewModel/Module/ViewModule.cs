using Ninject.Modules;
using OpenHab.Wpf.ViewModel.ViewModels;

namespace OpenHab.Wpf.ViewModel.Module
{
    public class ViewModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ServerViewModel>().ToSelf().InSingletonScope();
            Bind<ItemsViewModel>().ToSelf().InSingletonScope();
            Bind<ThingsViewModel>().ToSelf().InSingletonScope();

            Bind<ThingViewModel>().ToSelf();
            Bind<ItemViewModel>().ToSelf();
            Bind<StateDescriptionViewModel>().ToSelf();
            Bind<OptionViewModel>().ToSelf();
            Bind<ChannelViewModel>().ToSelf();
            Bind<FirmwareViewModel>().ToSelf();
            Bind<StatusInfoViewModel>().ToSelf();
        }
    }
}