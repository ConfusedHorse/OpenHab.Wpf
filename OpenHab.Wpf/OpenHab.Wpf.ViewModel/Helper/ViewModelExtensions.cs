using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenHab.Wpf.ViewModel.ViewModels;
using OpenHab.Wpf.ViewModel.ViewModels.Custom;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.Helper
{
    public static class ViewModelExtensions
    {
        #region ToViewModel

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

        public static TriggerViewModel ToTriggerViewModel(this ItemViewModel itemViewModel)
        {
            return new TriggerViewModel(itemViewModel);
        }

        public static TimeOfDayViewModel ToTriggerViewModel(this TimeOfDayViewModel timeOfDayViewModel)
        {
            return timeOfDayViewModel.GenerateTrigger();
        }

        public static TriggerViewModel[] ToTriggerViewModels(this TimeCombinedViewModel timeCombinedViewModel)
        {
            return timeCombinedViewModel.GenerateTriggers();
        }

        public static ConditionViewModel ToConditionViewModel(this ItemViewModel itemViewModel)
        {
            return new ConditionViewModel(itemViewModel);
        }

        public static ConditionViewModel ToConditionViewModel(this DayOfWeekViewModel dayOfWeekViewModel)
        {
            return dayOfWeekViewModel.GenerateCondition();
        }

        public static ConditionViewModel[] ToConditionViewModels(this TimeCombinedViewModel timeViewModel)
        {
            return timeViewModel.GenerateConditions();
        }

        public static ActionViewModel ToActionViewModel(this ItemViewModel itemViewModel)
        {
            return new ActionViewModel(itemViewModel);
        }

        public static TriggerViewModel ToViewModel(this Trigger trigger)
        {
            switch (trigger.Type)
            {
                case "timer.TimeOfDayTrigger": return new TimeOfDayViewModel(trigger);
                //case "jsr223.ScriptedTrigger": return new TriggerViewModel(trigger);
                //case "timer.GenericCronTrigger": return new TriggerViewModel(trigger);
                case "core.ItemCommandTrigger": return new TriggerViewModel(trigger); //handled by TriggerViewModel.TriggerSource
                //case "core.GenericEventTrigger": return new TriggerViewModel(trigger);
                case "core.ItemStateUpdateTrigger": return new TriggerViewModel(trigger); //handled by TriggerViewModel.TriggerSource
                case "core.ItemStateChangeTrigger": return new TriggerViewModel(trigger); //handled by TriggerViewModel.TriggerSource
                //case "core.ChannelEventTrigger": return new TriggerViewModel(trigger);
                default: return new TriggerViewModel(trigger);
            }
        }

        public static ObservableCollection<TriggerViewModel> ToViewModels(this IEnumerable<Trigger> triggers)
        {
            return new ObservableCollection<TriggerViewModel>(triggers.Select(t => t?.ToViewModel()));
        }

        public static ConditionViewModel ToViewModel(this Condition condition)
        {
            switch (condition.Type)
            {
                //case "core.GenericEventCondition": return new ConditionViewModel(condition);
                //case "script.ScriptCondition": return new ConditionViewModel(condition);
                case "timer.DayOfWeekCondition": return new DayOfWeekViewModel(condition);
                //case "jsr223.ScriptedCondition": return new ConditionViewModel(condition);
                //case "core.ItemStateCondition": return new ConditionViewModel(condition);
                //case "core.GenericCompareCondition": return new ConditionViewModel(condition);
                default: return new ConditionViewModel(condition);
            }
        }

        public static ObservableCollection<ConditionViewModel> ToViewModels(this IEnumerable<Condition> conditions)
        {
            return new ObservableCollection<ConditionViewModel>(conditions.Select(c => c?.ToViewModel()));
        }

        public static ActionViewModel ToViewModel(this Action action)
        {
            switch (action.Type)
            {
                //case "media.PlayAction": return new ActionViewModel(action);
                //case "core.ItemCommandAction": return new ActionViewModel(action);
                //case "media.SayAction": return new ActionViewModel(action);
                //case "jsr223.ScriptedAction": return new ActionViewModel(action);
                //case "script.ScriptAction": return new ActionViewModel(action);
                //case "core.RunRuleAction": return new ActionViewModel(action);
                //case "core.RuleEnablementAction": return new ActionViewModel(action);
                default: return new ActionViewModel(action);
            }
        }

        public static ObservableCollection<ActionViewModel> ToViewModels(this IEnumerable<Action> actions)
        {
            return new ObservableCollection<ActionViewModel>(actions.Select(a => a?.ToViewModel()));
        }

        public static RuleViewModel ToViewModel(this Rule rule)
        {
            return new RuleViewModel(rule);
        }

        public static ObservableCollection<RuleViewModel> ToViewModels(this IEnumerable<Rule> rules)
        {
            return new ObservableCollection<RuleViewModel>(rules.Select(r => r?.ToViewModel()));
        }

        #endregion

        #region FromViewModel

        public static List<string> FromViewModels(this ObservableCollection<string> list)
        {
            return list.ToList();
        }

        public static Rule FromViewModel(this RuleViewModel ruleViewModel)
        {
            return DataModelFactory.Create(ruleViewModel);
        }

        public static Action FromViewModel(this ActionViewModel action)
        {
            return DataModelFactory.Create(action);
        }

        public static List<Action> FromViewModels(this ObservableCollection<ActionViewModel> actions)
        {
            return actions.Select(a => a?.FromViewModel()).ToList();
        }

        public static Trigger FromViewModel(this TriggerViewModel trigger)
        {
            return DataModelFactory.Create(trigger);
        }

        public static List<Trigger> FromViewModels(this ObservableCollection<TriggerViewModel> triggers)
        {
            return triggers.Select(t => t?.FromViewModel()).ToList();
        }

        public static Condition FromViewModel(this ConditionViewModel condition)
        {
            return DataModelFactory.Create(condition);
        }

        public static List<Condition> FromViewModels(this ObservableCollection<ConditionViewModel> conditions)
        {
            return conditions.Select(c => c?.FromViewModel()).ToList();
        }

        public static Output FromViewModel(this OutputViewModel output)
        {
            return DataModelFactory.Create(output);
        }

        public static List<Output> FromViewModels(this ObservableCollection<OutputViewModel> outputs)
        {
            return outputs.Select(o => o?.FromViewModel()).ToList();
        }

        public static ConfigDescription FromViewModel(this ConfigDescriptionViewModel configDescription)
        {
            return DataModelFactory.Create(configDescription);
        }

        public static List<ConfigDescription> FromViewModels(this ObservableCollection<ConfigDescriptionViewModel> configDescriptions)
        {
            return configDescriptions.Select(cd => cd?.FromViewModel()).ToList();
        }

        public static StatusInfo FromViewModel(this StatusInfoViewModel statusInfo)
        {
            return DataModelFactory.Create(statusInfo);
        }

        public static List<StatusInfo> FromViewModels(this ObservableCollection<StatusInfoViewModel> statusInfos)
        {
            return statusInfos.Select(si => si?.FromViewModel()).ToList();
        }

        public static Option FromViewModel(this OptionViewModel option)
        {
            return DataModelFactory.Create(option);
        }

        public static List<Option> FromViewModels(this ObservableCollection<OptionViewModel> options)
        {
            return options.Select(o => o?.FromViewModel()).ToList();
        }

        public static FilterCriteria FromViewModel(this FilterCriteriaViewModel filterCriteria)
        {
            return DataModelFactory.Create(filterCriteria);
        }

        public static List<FilterCriteria> FromViewModels(this ObservableCollection<FilterCriteriaViewModel> filterCriterias)
        {
            return filterCriterias.Select(si => si?.FromViewModel()).ToList();
        }

        #endregion
    }
}