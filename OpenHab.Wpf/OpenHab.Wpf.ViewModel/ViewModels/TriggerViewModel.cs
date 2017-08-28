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
    public class TriggerViewModel : ViewModelBase
    {
        #region Fields
        
        private ObservableCollection<ConfigDescriptionViewModel> _configDescriptions;
        private string _description;
        private string _label;
        private ObservableCollection<string> _tags;
        private string _visibility;
        private ObservableCollection<OutputViewModel> _outputs;
        private string _uid;
        private string _type;
        private object _configuration;
        private string _id;

        private readonly Trigger _trigger;

        #endregion

        public TriggerViewModel(Trigger trigger)
        {
            Id = trigger.Id;
            Configuration = trigger.Configuration;
            Type = trigger.Type;

            //ModuleType members
            Uid = trigger.Uid;
            Outputs = trigger.Outputs?.ToViewModels();
            Visibility = trigger.Visibility;
            Tags = trigger.Tags?.ToViewModels();
            Label = trigger.Label;
            Description = trigger.Description;
            ConfigDescriptions = trigger.ConfigDescriptions?.ToViewModels();

            _trigger = trigger;
            InitializeCommands();
        }

        public TriggerViewModel(ItemViewModel itemViewModel)
        {
            var itemName = itemViewModel.Name;

            Configuration = new
            {
                itemName = itemName
            };
            Id = Guid.NewGuid().ToString();
            Label = string.Format(Properties.Resources.ItemStateUpdateTriggerDefaultLabel, itemName);
            Description = string.Format(Properties.Resources.ItemStateUpdateTriggerDefaultDescription, itemName);
            Type = "core.ItemStateUpdateTrigger";

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

        public DelegateCommand DeleteTriggerCommand { get; private set; }

        private void InitializeCommands()
        {
            DeleteTriggerCommand = new DelegateCommand(DeleteAction);
        }

        private void DeleteAction()
        {
            var currentRule = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
            currentRule?.RemoveTrigger(this);
        }

        #endregion

        #region Private Methods

        private async void LoadModuleTypeInformationAsync()
        {
            var moduleType = await Task.Run(() => _trigger.GetModuleType());
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