using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Framework.UI.Controls;
using Framework.UI.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Ninject;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.Helper;
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
            Name = $"{Properties.Resources.NewRule}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}";
            Enabled = false;

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
                Enabled = (bool)ruleEnabled;

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
                else this.FromViewModel()?.Update();
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
        public DelegateCommand<object> AddActionFromDragDataCommand { get; private set; }

        private void InitializeCommands()
        {
            RunRuleCommand = new DelegateCommand(RunRule, CanRunRule);
            DeleteRuleCommand = new DelegateCommand(DeleteRuleAsync, CanDeleteRule);
            SaveRuleCommand = new DelegateCommand(SaveChangesAsync, CanSaveRule);
            AddActionFromDragDataCommand = new DelegateCommand<object>(AddActionFromDragData);
        }

        private bool CanSaveRule()
        {
            return UnsavedChanges;
        }

        private void RunRule()
        {
            _rule.Run();
        }

        private bool CanRunRule()
        {
            return Uid != null;
        }

        private async void DeleteRuleAsync()
        {
            if (Uid == null)
            {
                var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
                rulesViewModel.RemovePhantomRule(this);
            }
            else
            {
                var owner = Application.Current.MainWindow;
                var result = await MessageDialog.ShowAsync(Properties.Resources.DeleteRuleHeader,
                    string.Format(Properties.Resources.DeleteRuleContent, Name ?? Uid), MessageBoxButton.YesNo,
                    owner: owner);
                if (result == MessageBoxResult.Yes)
                {
                    _rule?.Delete();
                }
            }
        }

        private bool CanDeleteRule()
        {
            return !IsRuleDummy;
        }

        private static void AddActionFromDragData(object data)
        {
            switch (data)
            {
                case ItemViewModel i:
                    AddActionFromItem(i);
                    break;
                //TODO add other ModuleTypes here
            }
        }

        #endregion

        private static void AddActionFromItem(ItemViewModel itemViewModel)
        {
            var itemCommandAction = itemViewModel.ToActionViewModel();
            var rulesViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>();
            var currentRule = rulesViewModel.CurrentRule;
            currentRule.UnsavedChanges = true;
            DispatcherHelper.RunAsync(() => currentRule.Actions.Add(itemCommandAction));
        }

        #endregion
    }
}