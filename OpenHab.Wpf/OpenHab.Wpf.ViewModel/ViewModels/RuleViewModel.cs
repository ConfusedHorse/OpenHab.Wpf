using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

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
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
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

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}