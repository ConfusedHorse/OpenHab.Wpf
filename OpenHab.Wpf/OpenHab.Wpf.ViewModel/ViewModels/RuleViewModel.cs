﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Framework;
using Framework.UI.Controls;
using Framework.UI.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Ninject;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHab.Wpf.ViewModel.Properties;
using OpenHab.Wpf.ViewModel.ViewModels.Custom;
using OpenHAB.NetRestApi.Models;
using OpenHAB.NetRestApi.Models.Events;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class RuleViewModel : ViewModelBase
    {
        #region Fields

        private StatusInfoViewModel _status;
        private bool _enabled;
        private string _description;
        private string _visibility;
        private ObservableCollection<string> _tags;
        private string _name;
        private string _uid;
        private string _templateUid;
        private ObservableCollection<ConfigDescriptionViewModel> _configDescriptions;
        private object _configuration;
        private ObservableCollection<ActionViewModel> _actions;
        private ObservableCollection<ConditionViewModel> _conditions;
        private ObservableCollection<TriggerViewModel> _triggers;

        private Rule _rule;
        private bool _isRuleDummy;
        private bool _unsavedChanges;

        #endregion

        public RuleViewModel(Rule rule)
        {
            Triggers = rule.Triggers?.ToViewModels();
            Conditions = rule.Conditions?.ToViewModels();
            Actions = rule.Actions?.ToViewModels();
            Configuration = rule.Configuration;
            ConfigDescriptions = rule.ConfigDescriptions?.ToViewModels();
            TemplateUid = rule.TemplateUid;
            Uid = rule.Uid;
            Name = rule.Name;
            Tags = rule.Tags?.ToViewModels();
            Visibility = rule.Visibility;
            Description = rule.Description;
            Enabled = rule.Enabled;
            Status = rule.Status?.ToViewModel();

            _rule = rule;
            InitializeEventHandlers();
            InitializeCommands();
            UnsavedChanges = false;
        }

        public RuleViewModel()
        {
            //Create Dummy
            Triggers = new ObservableCollection<TriggerViewModel>();
            Conditions = new ObservableCollection<ConditionViewModel>();
            Actions = new ObservableCollection<ActionViewModel>();
            Name = $"{Resources.NewRule}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}";
            Enabled = false;
            Status = StatusInfoViewModel.Default;

            IsRuleDummy = true;
            InitializeEventHandlers();
            InitializeCommands();
            UnsavedChanges = false;
        }

        public void Update(Rule rule)
        {
            Triggers = rule.Triggers?.ToViewModels();
            Conditions = rule.Conditions?.ToViewModels();
            Actions = rule.Actions?.ToViewModels();
            Configuration = rule.Configuration;
            ConfigDescriptions = rule.ConfigDescriptions?.ToViewModels();
            TemplateUid = rule.TemplateUid;
            Uid = rule.Uid;
            Name = rule.Name;
            Tags = rule.Tags?.ToViewModels();
            Visibility = rule.Visibility;
            Description = rule.Description;
            Enabled = rule.Enabled;
            Status = rule.Status?.ToViewModel();

            _rule = rule;
            UnsavedChanges = false;
        }

        #region Properties

        public ObservableCollection<TriggerViewModel> Triggers
        {
            get => _triggers;
            set
            {
                _triggers = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ConditionViewModel> Conditions
        {
            get => _conditions;
            set
            {
                _conditions = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ActionViewModel> Actions
        {
            get => _actions;
            set
            {
                _actions = value;
                RaisePropertyChanged();
            }
        }

        public object Configuration
        {
            get => _configuration;
            set
            {
                _configuration = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ConfigDescriptionViewModel> ConfigDescriptions
        {
            get => _configDescriptions;
            set
            {
                _configDescriptions = value;
                RaisePropertyChanged();
            }
        }

        public string TemplateUid
        {
            get => _templateUid;
            set
            {
                _templateUid = value;
                RaisePropertyChanged();
            }
        }

        public string Uid
        {
            get => _uid;
            set
            {
                _uid = value;
                RaisePropertyChanged();
                DispatcherHelper.RunAsync(() => RunRuleCommand?.RaiseCanExecuteChanged());
                RaisePropertyChanged(() => CanToggleRule);
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                UnsavedChanges = true;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                UnsavedChanges = true;
                RaisePropertyChanged();
            }
        }

        public string Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                UnsavedChanges = true;
                RaisePropertyChanged();
            }
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                RaisePropertyChanged();
            }
        }

        public StatusInfoViewModel Status
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRuleDummy
        {
            get => _isRuleDummy;
            set
            {
                _isRuleDummy = value;
                DispatcherHelper.RunAsync(() => DeleteRuleCommand?.RaiseCanExecuteChanged());
                RaisePropertyChanged();
            }
        }

        public bool CanToggleRule => Uid != null;

        public bool UnsavedChanges
        {
            get => _unsavedChanges;
            set
            {
                _unsavedChanges = value;
                DispatcherHelper.RunAsync(() => SaveRuleCommand?.RaiseCanExecuteChanged());
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        public void ToggleRule(bool? ruleEnabled)
        {
            if (ruleEnabled != null && ruleEnabled != _enabled)
                Enabled = (bool) ruleEnabled;

            if (_enabled)
                _rule.Enable();
            else
                _rule.Disable();
        }

        public void RemoveAction(ActionViewModel actionViewModel)
        {
            if (Actions.IsNullOrEmpty()) return;
            Actions.Remove(actionViewModel);
            UnsavedChanges = true;
        }

        public void RemoveTrigger(TriggerViewModel triggerViewModel)
        {
            if (Triggers.IsNullOrEmpty()) return;
            Triggers.Remove(triggerViewModel);
            UnsavedChanges = true;
        }

        public void RemoveCondition(ConditionViewModel conditionViewModel)
        {
            if (Conditions.IsNullOrEmpty()) return;
            Conditions.Remove(conditionViewModel);
            UnsavedChanges = true;
        }

        public void SaveChangesAsync()
        {
            Task.Run(() =>
            {
                if (Uid == null)
                {
                    var result = this.FromViewModel()?.Create();
                    if (result == null || result != string.Empty) return;

                    var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
                    rulesViewModel.RemovePhantomRule(this);
                }
                else
                {
                    this.FromViewModel()?.Update();
                }
            });

            UnsavedChanges = false;
        }

        #endregion

        #region Private Methods

        #region Events

        private void InitializeEventHandlers()
        {
            if (_rule == null) return;
            _rule.Updated += OnUpdated;

            _rule.InitializeEvents();
        }

        private void OnUpdated(object sender, RuleUpdatedEvent eventObject)
        {
            Update(eventObject.NewRule);
        }

        #endregion

        #region Commands

        public DelegateCommand RunRuleCommand { get; private set; }
        public DelegateCommand DeleteRuleCommand { get; private set; }
        public DelegateCommand SaveRuleCommand { get; private set; }
        public DelegateCommand<object> AddTriggerFromDragDataCommand { get; private set; }
        public DelegateCommand<object> AddConditionFromDragDataCommand { get; private set; }
        public DelegateCommand<object> AddActionFromDragDataCommand { get; private set; }

        private void InitializeCommands()
        {
            RunRuleCommand = new DelegateCommand(RunRule, CanRunRule);
            DeleteRuleCommand = new DelegateCommand(DeleteRuleAsync, CanDeleteRule);
            SaveRuleCommand = new DelegateCommand(SaveChangesAsync, CanSaveRule);
            AddTriggerFromDragDataCommand = new DelegateCommand<object>(AddTriggerFromDragData, CanExecuteDropTrigger);
            AddConditionFromDragDataCommand = new DelegateCommand<object>(AddConditionFromDragData, CanExecuteDropCondition);
            AddActionFromDragDataCommand = new DelegateCommand<object>(AddActionFromDragData, CanExecuteDropAction);
        }

        #region Validation

        private bool CanSaveRule()
        {
            return UnsavedChanges;
        }

        private bool CanRunRule()
        {
            return Uid != null;
        }

        private bool CanDeleteRule()
        {
            return !IsRuleDummy;
        }

        private static bool CanExecuteDropTrigger(object data)
        {
            switch (data)
            {
                case ItemViewModel _:
                    return true;
                case TimeCombinedViewModel _:
                    return true;
                case TimeOfDayViewModel _:
                    return true;
                //TODO add other operations here
            }
            return false;
        }

        private static bool CanExecuteDropCondition(object data)
        {
            switch (data)
            {
                case ItemViewModel _:
                    return true;
                case TimeCombinedViewModel _:
                    return true;
                case DayOfWeekViewModel _:
                    return true;
                case TimePeriodViewModel _:
                    return true;
                //TODO add other operations here
            }
            return false;
        }

        private static bool CanExecuteDropAction(object data)
        {
            switch (data)
            {
                case ItemViewModel _:
                    return true;
                //TODO add other operations here
            }
            return false;
        }

        #endregion

        #region Actions

        private void RunRule()
        {
            if (_enabled)
            {
                _rule.Run();
            }
            else
            {
                _rule.Enable();
                _rule.Run();
                _rule.Disable();
            }
        }

        private async void DeleteRuleAsync()
        {
            if (Uid == null || Triggers.IsNullOrEmpty() && Conditions.IsNullOrEmpty() && Actions.IsNullOrEmpty())
            {
                var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
                rulesViewModel.RemovePhantomRule(this);
            }
            else
            {
                var owner = Application.Current.MainWindow;
                var result = await MessageDialog.ShowAsync(Resources.DeleteRuleHeader,
                    string.Format(Resources.DeleteRuleContent, Name ?? Uid), MessageBoxButton.YesNo,
                    owner: owner);
                if (result == MessageBoxResult.Yes)
                    _rule?.Delete();
            }
        }

        private static void AddTriggerFromDragData(object data)
        {
            HandleDummyAsRule();
            switch (data)
            {
                case ItemViewModel i:
                    AddTriggerFromItem(i);
                    break;
                case TimeCombinedViewModel tc:
                    AddTriggersAndConditionsFromTimer(tc);
                    break;
                case TimeOfDayViewModel tod:
                    AddTriggerFromTrigger(tod);
                    break;
                //TODO add other ModuleTypes here
            }
        }

        private static void AddConditionFromDragData(object data)
        {
            HandleDummyAsRule();
            switch (data)
            {
                case ItemViewModel i:
                    AddConditionFromItem(i);
                    break;
                case TimeCombinedViewModel tc:
                    AddTriggersAndConditionsFromTimer(tc);
                    break;
                case DayOfWeekViewModel dow:
                    AddConditionFromCondition(dow);
                    break;
                case TimePeriodViewModel tp:
                    AddConditionFromCondition(tp);
                    break;
                //TODO add other ModuleTypes here
            }
        }

        private static void AddActionFromDragData(object data)
        {
            HandleDummyAsRule();
            switch (data)
            {
                case ItemViewModel i:
                    AddActionFromItem(i);
                    break;
                //TODO add other ModuleTypes here
            }
        }

        #endregion

        #region Other
        
        private static void HandleDummyAsRule()
        {
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            if (rulesViewModel.CurrentRule.IsRuleDummy)
                rulesViewModel.CreateNewRule();
        }

        #endregion

        #region Apply Rule Components

        private static void AddTriggerFromItem(ItemViewModel itemViewModel)
        {
            var itemStateUpdateTrigger = itemViewModel.ToTriggerViewModel();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            currentRule.UnsavedChanges = true;
            DispatcherHelper.RunAsync(() => currentRule.Triggers.Add(itemStateUpdateTrigger));
        }

        private static void AddTriggerFromTrigger(TimeOfDayViewModel timeOfDayViewModel)
        {
            var timeOfDayTrigger = timeOfDayViewModel.ToTriggerViewModel();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            currentRule.UnsavedChanges = true;
            DispatcherHelper.RunAsync(() => currentRule.Triggers.Add(timeOfDayTrigger));
        }

        private static void AddConditionFromItem(ItemViewModel itemViewModel)
        {
            var itemStateCondition = itemViewModel.ToConditionViewModel();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            currentRule.UnsavedChanges = true;
            DispatcherHelper.RunAsync(() => currentRule.Conditions.Add(itemStateCondition));
        }

        private static void AddConditionFromCondition(DayOfWeekViewModel dayOfWeekViewModel)
        {
            var dayOfWeekCondition = dayOfWeekViewModel.ToConditionViewModel();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            currentRule.UnsavedChanges = true;
            DispatcherHelper.RunAsync(() => currentRule.Conditions.Add(dayOfWeekCondition));
        }

        private static void AddConditionFromCondition(TimePeriodViewModel timePeriodViewModel)
        {
            var timePeriodCondition = timePeriodViewModel.ToConditionViewModel();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            currentRule.UnsavedChanges = true;
            DispatcherHelper.RunAsync(() => currentRule.Conditions.Add(timePeriodCondition));
        }

        private static void AddTriggersAndConditionsFromTimer(TimeCombinedViewModel timeViewModel)
        {
            var timedTriggers = timeViewModel.ToTriggerViewModels();
            var timedConditions = timeViewModel.ToConditionViewModels();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            if (timedTriggers.Any())
            {
                currentRule.UnsavedChanges = true;
                DispatcherHelper.RunAsync(() => currentRule.Triggers.AddRange(timedTriggers));
            }
            if (timedConditions.Any())
            {
                currentRule.UnsavedChanges = true;
                DispatcherHelper.RunAsync(() => currentRule.Conditions.AddRange(timedConditions));
            }
        }

        private static void AddActionFromItem(ItemViewModel itemViewModel)
        {
            var itemCommandAction = itemViewModel.ToActionViewModel();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            currentRule.UnsavedChanges = true;
            DispatcherHelper.RunAsync(() => currentRule.Actions.Add(itemCommandAction));
        }

        #endregion

        #endregion

        #endregion
    }
}