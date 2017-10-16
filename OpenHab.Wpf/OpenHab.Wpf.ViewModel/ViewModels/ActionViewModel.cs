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
using Action = OpenHAB.NetRestApi.Models.Action;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ActionViewModel : ViewModelBase
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

        private readonly Action _action;
        private object _actionSource;

        #endregion

        public ActionViewModel(Action action)
        {
            Id = action.Id;
            Configuration = action.Configuration;
            Type = action.Type;
            Inputs = action.Inputs;

            //ModuleType members
            Uid = action.Uid;
            Outputs = action.Outputs?.ToViewModels();
            Visibility = action.Visibility;
            Tags = action.Tags?.ToViewModels();
            Label = action.Label;
            Description = action.Description;
            ConfigDescriptions = action.ConfigDescriptions?.ToViewModels();

            _action = action;
            InitializeActionSource(action);
            InitializeCommands();
        }

        public ActionViewModel(ItemViewModel itemViewModel)
        {
            var itemName = itemViewModel.Name;
            var state = itemViewModel.State;
            
            Configuration = new
            {
                itemName = itemName,
                command = state
            };
            Id = Guid.NewGuid().ToString();
            Label = string.Format(Properties.Resources.ItemCommandActionDefaultLabel, itemName, state);
            Description = string.Format(Properties.Resources.ItemCommandActionDefaultDescription, itemName, state);
            Type = "core.ItemCommandAction";

            InitializeActionSource(itemViewModel);
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

        public object ActionSource
        {
            get => _actionSource;
            set
            {
                _actionSource = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Commands

        public DelegateCommand DeleteActionCommand { get; private set; }

        private void InitializeCommands()
        {
            DeleteActionCommand = new DelegateCommand(DeleteAction);
        }

        private void DeleteAction()
        {
            var currentRule = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
            currentRule?.RemoveAction(this);
        }

        #endregion

        #region Private Methods

        private void InitializeActionSource(Action action)
        {
            switch (action.Type)
            {
                case "media.PlayAction":
                    break;
                case "core.ItemCommandAction":
                    InitializeActionSourceFromItemAction(action);
                    break;
                case "media.SayAction":
                    break;
                case "jsr223.ScriptedAction":
                    break;
                case "script.ScriptAction":
                    break;
                case "core.RunRuleAction":
                    break;
                case "core.RuleEnablementAction":
                    break;
            }
        }

        private void InitializeActionSourceFromItemAction(Action action)
        {
            dynamic configuration = action.Configuration;
            string itemName = configuration.itemName;
            string itemState = configuration.command;
            if (itemName == null) return;
            var item = NinjectKernel.StandardKernel.Get<RestContext>().Client
                .ItemService.GetItem(itemName);
            var itemViewModel = new ItemViewModel(item, Synchronize.Disabled) {State = itemState};
            InitializeActionSource(itemViewModel);
        }

        private void InitializeActionSource(ItemViewModel itemViewModel)
        {
            var proxy = itemViewModel;
            if (itemViewModel.Direction != Synchronize.Disabled)
            { // is item from thing
                proxy = itemViewModel.GetProxy(Synchronize.Disabled);
            }

            ActionSource = proxy;
            proxy.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(ItemViewModel.State)) return;
                var source = (ItemViewModel) sender;
                Configuration = new
                {
                    itemName = source.Name,
                    command = source.State
                };

                Label = string.Format(Properties.Resources.ItemCommandActionDefaultLabel, source.Name, source.State);
                Description = string.Format(Properties.Resources.ItemCommandActionDefaultDescription, source.Name, source.State);
            };
        }

        private async void LoadModuleTypeInformationAsync()
        {
            var moduleType = await Task.Run(() => _action.GetModuleType());
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