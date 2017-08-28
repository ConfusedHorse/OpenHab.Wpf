using OpenHab.Wpf.ViewModel.ViewModels;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.Helper
{
    public static class ModelFactory
    {
        public static Rule Create(RuleViewModel ruleViewModel)
        {
            return new Rule
            {
                Triggers = ruleViewModel.Triggers?.FromViewModels(),
                Conditions = ruleViewModel.Conditions?.FromViewModels(),
                Actions = ruleViewModel.Actions?.FromViewModels(),
                Configuration = ruleViewModel.Configuration,
                ConfigDescriptions = ruleViewModel.ConfigDescriptions?.FromViewModels(),
                TemplateUid = ruleViewModel.TemplateUid,
                Uid = ruleViewModel.Uid,
                Name = ruleViewModel.Name,
                Tags = ruleViewModel.Tags?.FromViewModels(),
                Visibility = ruleViewModel.Visibility,
                Description = ruleViewModel.Description,
                Enabled = ruleViewModel.Enabled,
                Status = ruleViewModel.Status?.FromViewModel()
            };
        }

        public static Action Create(ActionViewModel actionViewModel)
        {
            return new Action
            {
                Id = actionViewModel.Id,
                Configuration = actionViewModel.Configuration,
                Type = actionViewModel.Type,
                Inputs = actionViewModel.Inputs,

                //ModuleType members
                Uid = actionViewModel.Uid,
                Outputs = actionViewModel.Outputs?.FromViewModels(),
                Visibility = actionViewModel.Visibility,
                Tags = actionViewModel.Tags?.FromViewModels(),
                Label = actionViewModel.Label,
                Description = actionViewModel.Description,
                ConfigDescriptions = actionViewModel.ConfigDescriptions?.FromViewModels()
            };
        }

        public static Trigger Create(TriggerViewModel triggerViewModel)
        {
            return new Trigger
            {
                Id = triggerViewModel.Id,
                Configuration = triggerViewModel.Configuration,
                Type = triggerViewModel.Type,

                //ModuleType members
                Uid = triggerViewModel.Uid,
                Outputs = triggerViewModel.Outputs?.FromViewModels(),
                Visibility = triggerViewModel.Visibility,
                Tags = triggerViewModel.Tags?.FromViewModels(),
                Label = triggerViewModel.Label,
                Description = triggerViewModel.Description,
                ConfigDescriptions = triggerViewModel.ConfigDescriptions?.FromViewModels()
            };
        }

        public static Condition Create(ConditionViewModel conditionViewModel)
        {
            return new Condition
            {
                Id = conditionViewModel.Id,
                Configuration = conditionViewModel.Configuration,
                Type = conditionViewModel.Type,
                Inputs = conditionViewModel.Inputs,

                //ModuleType members
                Uid = conditionViewModel.Uid,
                Outputs = conditionViewModel.Outputs?.FromViewModels(),
                Visibility = conditionViewModel.Visibility,
                Tags = conditionViewModel.Tags?.FromViewModels(),
                Label = conditionViewModel.Label,
                Description = conditionViewModel.Description,
                ConfigDescriptions = conditionViewModel.ConfigDescriptions?.FromViewModels()
            };
        }

        public static Output Create(OutputViewModel outputViewModel)
        {
            return new Output
            {
                Name = outputViewModel.Name,
                Type = outputViewModel.Type,
                Tags = outputViewModel.Tags?.FromViewModels(),
                Label = outputViewModel.Label,
                Description = outputViewModel.Description,
                Reference = outputViewModel.Reference
            };
        }

        public static ConfigDescription Create(ConfigDescriptionViewModel configDescriptionViewModel)
        {
            return new ConfigDescription
            {
                Name = configDescriptionViewModel.Name,
                Type = configDescriptionViewModel.Type,
                GroupName = configDescriptionViewModel.GroupName,
                Pattern = configDescriptionViewModel.Pattern,
                Required = configDescriptionViewModel.Required,
                ReadOnly = configDescriptionViewModel.ReadOnly,
                Multiple = configDescriptionViewModel.Multiple,
                MultipleLimit = configDescriptionViewModel.MultipleLimit,
                Unit = configDescriptionViewModel.Unit,
                UnitLabel = configDescriptionViewModel.UnitLabel,
                Context = configDescriptionViewModel.Context,
                Label = configDescriptionViewModel.Label,
                Description = configDescriptionViewModel.Description,
                Options = configDescriptionViewModel.Options?.FromViewModels(),
                FilterCriteria = configDescriptionViewModel.FilterCriteria?.FromViewModels(),
                LimitToOptions = configDescriptionViewModel.LimitToOptions,
                Advanced = configDescriptionViewModel.Advanced,
                StepSize = configDescriptionViewModel.StepSize,
                Verifyable = configDescriptionViewModel.Verifyable,
                Default = configDescriptionViewModel.Default,
                Minimum = configDescriptionViewModel.Minimum,
                Maximum = configDescriptionViewModel.Maximum
        };
        }

        public static StatusInfo Create(StatusInfoViewModel statusInfoViewModel)
        {
            return new StatusInfo
            {
                Status = statusInfoViewModel.Status,
                StatusDetail = statusInfoViewModel.StatusDetail,
                Description = statusInfoViewModel.Description
            };
        }

        public static Option Create(OptionViewModel optionViewModel)
        {
            return new Option
            {
                Label = optionViewModel.Label,
                Value = optionViewModel.Value
            };
        }

        public static FilterCriteria Create(FilterCriteriaViewModel filterCriteriaViewModel)
        {
            return new FilterCriteria
            {
                Name = filterCriteriaViewModel.Name,
                Value = filterCriteriaViewModel.Value
            };
        }
    }
}