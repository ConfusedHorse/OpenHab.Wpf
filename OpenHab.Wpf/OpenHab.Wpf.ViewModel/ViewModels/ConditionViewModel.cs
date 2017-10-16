using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Framework.UI.Input;
using GalaSoft.MvvmLight;
using Ninject;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.Enums;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ConditionViewModel : ViewModelBase
    {
        #region Fields
        
        private ObservableCollection<ConfigDescriptionViewModel> _configDescriptions;
        private string _description;
        private string _label;
        private ObservableCollection<string> _tags;
        private string _visibility;
        private ObservableCollection<OutputViewModel> _outputs;
        private string _uid;
        private object _inputs;
        private string _type;
        private object _configuration;
        private string _id;

        private readonly Condition _condition;
        private object _conditionSource;
        private bool _isTool;

        #endregion

        public ConditionViewModel(Condition condition)
        {
            Id = condition.Id;
            Configuration = condition.Configuration;
            Type = condition.Type;
            Inputs = condition.Inputs;

            //ModuleType members
            Uid = condition.Uid;
            Outputs = condition.Outputs?.ToViewModels();
            Visibility = condition.Visibility;
            Tags = condition.Tags?.ToViewModels();
            Label = condition.Label;
            Description = condition.Description;
            ConfigDescriptions = condition.ConfigDescriptions?.ToViewModels();

            _condition = condition;
            InitializeConditionSource(condition);
            InitializeCommands();
        }

        public ConditionViewModel(ItemViewModel itemViewModel)
        {
            var itemName = itemViewModel.Name;
            var state = itemViewModel.State;

            Configuration = new
            {
                itemName = itemName,
                command = state
            };
            Id = Guid.NewGuid().ToString();
            Label = string.Format(Properties.Resources.ItemStateConditionDefaultLabel, itemName, state);
            Description = string.Format(Properties.Resources.ItemStateConditionDefaultDescription, itemName, state);
            Type = "core.ItemStateCondition";

            InitializeConditionSource(itemViewModel);
            InitializeCommands();
        }

        internal ConditionViewModel()
        {
            InitializeCommands();
        }

        #region Properties

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        public object Configuration
        {
            get => _configuration;
            set
            {
                _configuration = value;
                if (_type == "script.ScriptCondition")
                {
                    var ruleViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
                    if (ruleViewModel != null)
                        ruleViewModel.UnsavedChanges = true;
                }
                RaisePropertyChanged();
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        public object Inputs
        {
            get => _inputs;
            set
            {
                _inputs = value;
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
            }
        }

        public ObservableCollection<OutputViewModel> Outputs
        {
            get => _outputs;
            set
            {
                _outputs = value;
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

        public ObservableCollection<string> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                RaisePropertyChanged();
            }
        }

        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                if (_type == "script.ScriptCondition")
                {
                    var ruleViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
                    if (ruleViewModel != null)
                        ruleViewModel.UnsavedChanges = true;
                }
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                if (_type == "script.ScriptCondition")
                {
                    var ruleViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
                    if (ruleViewModel != null)
                        ruleViewModel.UnsavedChanges = true;
                }
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

        public object ConditionSource
        {
            get => _conditionSource;
            set
            {
                _conditionSource = value;
                RaisePropertyChanged();
            }
        }

        public bool IsTool
        {
            get => _isTool;
            set
            {
                _isTool = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Commands

        public DelegateCommand DeleteConditionCommand { get; private set; }

        private void InitializeCommands()
        {
            DeleteConditionCommand = new DelegateCommand(DeleteCondition);
        }

        private void DeleteCondition()
        {
            var currentRule = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
            currentRule?.RemoveCondition(this);
        }

        #endregion

        #region Private Methods

        private void InitializeConditionSource(Condition condition)
        {
            switch (condition.Type)
            {
                case "core.GenericEventCondition":
                    break;
                case "script.ScriptCondition":
                    break;
                case "timer.DayOfWeekCondition":
                    break;
                case "jsr223.ScriptedCondition":
                    break;
                case "core.ItemStateCondition":
                    InitializeConditionSourceFromCondition(condition);
                    break;
                case "core.GenericCompareCondition":
                    break;
            }
        }

        private void InitializeConditionSourceFromCondition(Condition condition)
        {
            dynamic configuration = condition.Configuration;
            string itemName = configuration.itemName;
            string itemState = configuration.state;
            if (itemName == null) return;
            var item = NinjectKernel.StandardKernel.Get<RestContext>().Client
                .ItemService.GetItem(itemName);
            var itemViewModel = new ItemViewModel(item, Synchronize.Disabled) { State = itemState };
            InitializeConditionSource(itemViewModel);
        }

        private void InitializeConditionSource(ItemViewModel itemViewModel)
        {
            var proxy = itemViewModel;
            if (itemViewModel.Direction != Synchronize.Disabled)
            { // is item from thing
                proxy = itemViewModel.GetProxy(Synchronize.Disabled);
            }

            ConditionSource = proxy;
            proxy.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(ItemViewModel.State)) return;
                var source = (ItemViewModel)sender;
                Configuration = new
                {
                    itemName = source.Name,
                    command = source.State
                };

                Label = string.Format(Properties.Resources.ItemStateConditionDefaultLabel, source.Name, source.State);
                Description = string.Format(Properties.Resources.ItemStateConditionDefaultDescription, source.Name, source.State);

                NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule.UnsavedChanges = true;
            };
        }

        private async void LoadModuleTypeInformationAsync()
        {
            var moduleType = await Task.Run(() => _condition.GetModuleType());
            UpdateModuleTypeInformation(moduleType);
        }

        private void UpdateModuleTypeInformation(ModuleType moduleType)
        {
            Uid = moduleType.Uid;
            Outputs = moduleType.Outputs?.ToViewModels();
            Visibility = moduleType.Visibility;
            Tags = moduleType.Tags?.ToViewModels();
            //Label = moduleType.Label;
            //Description = moduleType.Description;
            ConfigDescriptions = moduleType.ConfigDescriptions?.ToViewModels();
        }

        #endregion
    }
}