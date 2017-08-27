using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenHab.Wpf.ViewModel.ViewModels;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.Helper
{
    public static class ViewModelExtensions
    {
        public static ObservableCollection<string> ToViewModels(this IEnumerable<string> list)
        {
            return new ObservableCollection<string>(list);
        }

        public static ItemViewModel ToViewModel(this Item item)
        {
            return new ItemViewModel(item);
        }

        public static ObservableCollection<ItemViewModel> ToViewModels(this IEnumerable<Item> items)
        {
            return new ObservableCollection<ItemViewModel>(items.Select(i => i?.ToViewModel()));
        }

        public static StateDescriptionViewModel ToViewModel(this StateDescription stateDescription)
        {
            return stateDescription == null ? null : new StateDescriptionViewModel(stateDescription);
        }

        public static ThingViewModel ToViewModel(this Thing thing)
        {
            return new ThingViewModel(thing);
        }

        public static ObservableCollection<ThingViewModel> ToViewModels(this IEnumerable<Thing> things)
        {
            return new ObservableCollection<ThingViewModel>(things.Select(t => t?.ToViewModel()));
        }

        public static OptionViewModel ToViewModel(this Option option)
        {
            return new OptionViewModel(option);
        }

        public static ObservableCollection<OptionViewModel> ToViewModels(this IEnumerable<Option> options)
        {
            return new ObservableCollection<OptionViewModel>(options.Select(o => o?.ToViewModel()));
        }

        public static ChannelViewModel ToViewModel(this Channel channel)
        {
            return new ChannelViewModel(channel);
        }

        public static ObservableCollection<ChannelViewModel> ToViewModels(this IEnumerable<Channel> channels)
        {
            return new ObservableCollection<ChannelViewModel>(channels.Select(c => c?.ToViewModel()));
        }

        public static StatusInfoViewModel ToViewModel(this StatusInfo statusInfo)
        {
            return new StatusInfoViewModel(statusInfo);
        }

        public static FirmwareViewModel ToViewModel(this Firmware firmware)
        {
            return new FirmwareViewModel(firmware);
        }

        public static FilterCriteriaViewModel ToViewModel(this FilterCriteria filterCriteria)
        {
            return new FilterCriteriaViewModel(filterCriteria);
        }

        public static ObservableCollection<FilterCriteriaViewModel> ToViewModels(this IEnumerable<FilterCriteria> filterCriterias)
        {
            return new ObservableCollection<FilterCriteriaViewModel>(filterCriterias.Select(fc => fc?.ToViewModel()));
        }

        public static OutputViewModel ToViewModel(this Output output)
        {
            return new OutputViewModel(output);
        }

        public static ObservableCollection<OutputViewModel> ToViewModels(this IEnumerable<Output> outputs)
        {
            return new ObservableCollection<OutputViewModel>(outputs.Select(o => o?.ToViewModel()));
        }

        public static ConfigDescriptionViewModel ToViewModel(this ConfigDescription configDescription)
        {
            return new ConfigDescriptionViewModel(configDescription);
        }

        public static ObservableCollection<ConfigDescriptionViewModel> ToViewModels(this IEnumerable<ConfigDescription> configDescriptions)
        {
            return new ObservableCollection<ConfigDescriptionViewModel>(configDescriptions.Select(cd => cd?.ToViewModel()));
        }

        public static ActionViewModel ToViewModel(this Action action)
        {
            return new ActionViewModel(action);
        }

        public static ActionViewModel ToActionViewModel(this ItemViewModel itemViewModel)
        {
            return new ActionViewModel(itemViewModel);
        }

        public static ObservableCollection<ActionViewModel> ToViewModels(this IEnumerable<Action> actions)
        {
            return new ObservableCollection<ActionViewModel>(actions.Select(a => a?.ToViewModel()));
        }

        public static TriggerViewModel ToViewModel(this Trigger trigger)
        {
            return new TriggerViewModel(trigger);
        }

        public static ObservableCollection<TriggerViewModel> ToViewModels(this IEnumerable<Trigger> triggers)
        {
            return new ObservableCollection<TriggerViewModel>(triggers.Select(t => t?.ToViewModel()));
        }

        public static ConditionViewModel ToViewModel(this Condition condition)
        {
            return new ConditionViewModel(condition);
        }

        public static ObservableCollection<ConditionViewModel> ToViewModels(this IEnumerable<Condition> conditions)
        {
            return new ObservableCollection<ConditionViewModel>(conditions.Select(c => c?.ToViewModel()));
        }

        public static RuleViewModel ToViewModel(this Rule rule)
        {
            return new RuleViewModel(rule);
        }

        public static ObservableCollection<RuleViewModel> ToViewModels(this IEnumerable<Rule> rules)
        {
            return new ObservableCollection<RuleViewModel>(rules.Select(r => r?.ToViewModel()));
        }
    }
}