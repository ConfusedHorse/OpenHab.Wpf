using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class StateDescriptionViewModel : ViewModelBase
    {
        private int _maximum;
        private int _minimum;
        private ObservableCollection<OptionViewModel> _options;
        private string _pattern;
        private bool _readOnly;
        private int? _step;

        public StateDescriptionViewModel(StateDescription stateDescription)
        {
            Minimum = stateDescription.Minimum;
            Maximum = stateDescription.Maximum;
            Options = stateDescription.Options.ToViewModels();
            Pattern = stateDescription.Pattern;
            ReadOnly = stateDescription.ReadOnly;
            Step = stateDescription.Step;
        }

        #region Fields

        #endregion

        #region Properties

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

        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
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

        public int? Step
        {
            get => _step;
            set
            {
                _step = value;
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

        #endregion
    }
}