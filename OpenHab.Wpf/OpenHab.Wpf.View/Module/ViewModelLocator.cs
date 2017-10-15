using Ninject;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.ViewModels;

namespace OpenHab.Wpf.View.Module
{
    public class ViewModelLocator
    {
        public static ServerViewModel ServerViewModel => NinjectKernel.StandardKernel.Get<ServerViewModel>();
        public static ItemsViewModel ItemsViewModel => NinjectKernel.StandardKernel.Get<ItemsViewModel>();
        public static ThingsViewModel ThingsViewModel => NinjectKernel.StandardKernel.Get<ThingsViewModel>();
        public static RulesViewModel RulesViewModel => NinjectKernel.StandardKernel.Get<RulesViewModel>();
        public static TriggersViewModel TriggersViewModel => NinjectKernel.StandardKernel.Get<TriggersViewModel>();

        //TODO ConditionsViewModel & ActionsViewModel
        //public static ConditionsViewModel ConditionsViewModel => NinjectKernel.StandardKernel.Get<ConditionsViewModel>();
        //public static ActionsViewModel ActionsViewModel => NinjectKernel.StandardKernel.Get<ActionsViewModel>();
    }
}