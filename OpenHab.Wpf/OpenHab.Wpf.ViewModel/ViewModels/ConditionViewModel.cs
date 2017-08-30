using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Framework.UI.Input;
using GalaSoft.MvvmLight;
using Ninject;
using OpenHab.Wpf.CrossCutting.Module;
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
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
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