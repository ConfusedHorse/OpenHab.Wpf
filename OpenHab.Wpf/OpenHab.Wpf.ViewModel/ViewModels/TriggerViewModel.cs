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
using OpenHab.Wpf.ViewModel.ViewModels.Custom;
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
        private object _triggerSource;
        private bool _isTool;

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
            InitializeTriggerSource(trigger);
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

            InitializeTriggerSource(itemViewModel);
            InitializeCommands();
        }

        /// <summary>
        /// this is only meant to be invoked by <see cref="TimeCombinedViewModel"/>
        /// </summary>
        /// <param name="condition"></param>
        protected TriggerViewModel(Condition condition)
        {
            InitializeCommands();
        }

        /// <summary>
        /// this is only meant to be invoked by <see cref="TimeCombinedViewModel"/>
        /// </summary>
        internal TriggerViewModel()
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

        public object TriggerSource
        {
            get => _triggerSource;
            set
            {
                _triggerSource = value;
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

        private void InitializeTriggerSource(Trigger trigger)
        {
            switch (trigger.Type)
            {
                case "timer.TimeOfDayTrigger":
                    break;
                case "jsr223.ScriptedTrigger":
                    break;
                case "timer.GenericCronTrigger":
                    break;
                case "core.ItemCommandTrigger":
                    InitializeTriggerSourceFromItemTrigger(trigger);
                    break;
                case "core.GenericEventTrigger":
                    break;
                case "core.ItemStateUpdateTrigger":
                    InitializeTriggerSourceFromItemTrigger(trigger);
                    break;
                case "core.ItemStateChangeTrigger":
                    InitializeTriggerSourceFromItemTrigger(trigger);
                    break;
                case "core.ChannelEventTrigger":
                    break;
            }
        }

        private void InitializeTriggerSourceFromItemTrigger(Trigger trigger)
        {
            dynamic configuration = trigger.Configuration;
            string itemName = configuration.itemName;
            string itemState = configuration.state;
            if (itemName == null) return;
            var item = NinjectKernel.StandardKernel.Get<RestContext>().Client
                .ItemService.GetItem(itemName);
            var itemViewModel = new ItemViewModel(item, Synchronize.Disabled) { State = itemState };
            InitializeTriggerSource(itemViewModel);
        }

        private void InitializeTriggerSource(ItemViewModel itemViewModel)
        {
            var proxy = itemViewModel;
            if (itemViewModel.Direction != Synchronize.Disabled)
            { // is item from thing
                proxy = itemViewModel.GetProxy(Synchronize.Disabled);
            }

            TriggerSource = proxy;
            proxy.PropertyChanged += (sender, args) =>
            { // TODO this is not generic (implement discinct handlers)
                NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule.UnsavedChanges = true;
            };
        }

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