using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ConfigDescriptionViewModel : ViewModelBase
    {
        #region Fields

        private int _maximum;
        private int _minimum;
        private string _default;
        private bool _verifyable;
        private int _stepSize;
        private bool _advanced;
        private bool _limitToOptions;
        private ObservableCollection<FilterCriteriaViewModel> _filterCriteria;
        private ObservableCollection<OptionViewModel> _options;
        private string _description;
        private string _label;
        private string _context;
        private string _unitLabel;
        private string _unit;
        private int _multipleLimit;
        private bool _multiple;
        private bool _readOnly;
        private bool _required;
        private string _pattern;
        private string _groupName;
        private string _type;
        private string _name;

        private ConfigDescription _configDescription;

        #endregion

        public ConfigDescriptionViewModel(ConfigDescription configDescription)
        {
            Name = configDescription.Name;
            Type = configDescription.Type;
            GroupName = configDescription.GroupName;
            Pattern = configDescription.Pattern;
            Required = configDescription.Required;
            ReadOnly = configDescription.ReadOnly;
            Multiple = configDescription.Multiple;
            MultipleLimit = configDescription.MultipleLimit;
            Unit = configDescription.Unit;
            UnitLabel = configDescription.UnitLabel;
            Context = configDescription.Context;
            Label = configDescription.Label;
            Description = configDescription.Description;
            Options = configDescription.Options?.ToViewModels();
            FilterCriteria = configDescription.FilterCriteria?.ToViewModels();
            LimitToOptions = configDescription.LimitToOptions;
            Advanced = configDescription.Advanced;
            StepSize = configDescription.StepSize;
            Verifyable = configDescription.Verifyable;
            Default = configDescription.Default;
            Minimum = configDescription.Minimum;
            Maximum = configDescription.Maximum;

            _configDescription = configDescription;
        }

        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
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

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }

        public string Pattern
        {
            get => _pattern;
            set
            {
                _pattern = value;
                RaisePropertyChanged();
            }
        }

        public bool Required
        {
            get => _required;
            set
            {
                _required = value;
                RaisePropertyChanged();
            }
        }

        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                RaisePropertyChanged();
            }
        }

        public bool Multiple
        {
            get => _multiple;
            set
            {
                _multiple = value;
                RaisePropertyChanged();
            }
        }

        public int MultipleLimit
        {
            get => _multipleLimit;
            set
            {
                _multipleLimit = value;
                RaisePropertyChanged();
            }
        }

        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                RaisePropertyChanged();
            }
        }

        public string UnitLabel
        {
            get => _unitLabel;
            set
            {
                _unitLabel = value;
                RaisePropertyChanged();
            }
        }

        public string Context
        {
            get => _context;
            set
            {
                _context = value;
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

        public ObservableCollection<OptionViewModel> Options
        {
            get => _options;
            set
            {
                _options = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<FilterCriteriaViewModel> FilterCriteria
        {
            get => _filterCriteria;
            set
            {
                _filterCriteria = value;
                RaisePropertyChanged();
            }
        }

        public bool LimitToOptions
        {
            get => _limitToOptions;
            set
            {
                _limitToOptions = value;
                RaisePropertyChanged();
            }
        }

        public bool Advanced
        {
            get => _advanced;
            set
            {
                _advanced = value;
                RaisePropertyChanged();
            }
        }

        public int StepSize
        {
            get => _stepSize;
            set
            {
                _stepSize = value;
                RaisePropertyChanged();
            }
        }

        public bool Verifyable
        {
            get => _verifyable;
            set
            {
                _verifyable = value;
                RaisePropertyChanged();
            }
        }

        public string Default
        {
            get => _default;
            set
            {
                _default = value;
                RaisePropertyChanged();
            }
        }

        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                RaisePropertyChanged();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}