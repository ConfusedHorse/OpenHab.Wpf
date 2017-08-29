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
            Bind<RulesViewModel>().ToSelf().InSingletonScope();
            Bind<TriggersViewModel>().ToSelf().InSingletonScope();
            //Bind<ConditionsViewModel>().ToSelf().InSingletonScope();
            //Bind<ActionsViewModel>().ToSelf().InSingletonScope();

            Bind<ThingViewModel>().ToSelf();
            Bind<ItemViewModel>().ToSelf();
            Bind<RuleViewModel>().ToSelf();
            Bind<StateDescriptionViewModel>().ToSelf();
            Bind<OptionViewModel>().ToSelf();
            Bind<ChannelViewModel>().ToSelf();
            Bind<FirmwareViewModel>().ToSelf();
            Bind<StatusInfoViewModel>().ToSelf();
            Bind<ActionViewModel>().ToSelf();
            Bind<TriggerViewModel>().ToSelf();
            Bind<ConditionViewModel>().ToSelf();
            Bind<ConfigDescriptionViewModel>().ToSelf();
            Bind<FilterCriteriaViewModel>().ToSelf();
            Bind<OutputViewModel>().ToSelf();
        }
    }
}