using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.ViewModel.Helper;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class TriggersViewModel : ViewModelBase
    {
        #region Fields

        private readonly RestContext _restContext;
        private ObservableCollection<TriggerViewModel> _triggers = new ObservableCollection<TriggerViewModel>();
        private bool _isBusy;
        private string _filterCsv;
        private ObservableCollection<TriggerViewModel> _filteredTriggers = new ObservableCollection<TriggerViewModel>();

        #endregion

        public TriggersViewModel(RestContext restContext)
        {
            _restContext = restContext;
            //_restContext.ConnectionChanged += ;

            InitializeTriggers();
        }

        #region Properties

        public ObservableCollection<TriggerViewModel> Triggers
        {
            get => _triggers;
            set
            {
                _triggers = value;
                FilteredTriggers = value != null
                    ? new ObservableCollection<TriggerViewModel>(value)
                    : new ObservableCollection<TriggerViewModel>();
                RaisePropertyChanged();
            }
        }

        public string FilterCsv
        {
            get => _filterCsv;
            set
            {
                _filterCsv = value;
                UpdateFilteredThingsAsync();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TriggerViewModel> FilteredTriggers
        {
            get => _filteredTriggers;
            set
            {
                _filteredTriggers = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        private void UpdateFilteredThingsAsync()
        {
            DispatcherHelper.RunAsync(() =>
            {
                var updatedThings = _triggers.FilterBy(_filterCsv);
                var triggersToBeAdded = updatedThings.Where(ut => !_filteredTriggers.Contains(ut)).ToArray();
                var triggersToBeRemoved = _filteredTriggers.Where(ft => !updatedThings.Contains(ft)).ToArray();

                foreach (var triggerViewModel in triggersToBeAdded)
                {
                    FilteredTriggers.Add(triggerViewModel);
                }

                foreach (var thingViewModel in triggersToBeRemoved)
                {
                    FilteredTriggers.Remove(thingViewModel);
                }
            });
        }

        private void InitializeTriggers()
        {
            //TODO add all custom ModuleTypes here
            var triggers = new ObservableCollection<TriggerViewModel>
            {
                TimerViewModel.Default
            };

            Triggers = triggers;
        }

        #endregion
    }
}