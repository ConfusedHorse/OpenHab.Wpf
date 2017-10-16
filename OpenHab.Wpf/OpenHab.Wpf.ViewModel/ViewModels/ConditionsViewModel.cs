using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHab.Wpf.ViewModel.ViewModels.Custom;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ConditionsViewModel : ViewModelBase
    {
        #region Fields

        private readonly RestContext _restContext;
        private ObservableCollection<ConditionViewModel> _conditions = new ObservableCollection<ConditionViewModel>();
        private bool _isBusy;
        private string _filterCsv;
        private ObservableCollection<ConditionViewModel> _filteredConditions = new ObservableCollection<ConditionViewModel>();

        #endregion

        public ConditionsViewModel(RestContext restContext)
        {
            _restContext = restContext;
            //_restContext.ConnectionChanged += ;

            InitializeConditions();
        }

        #region Properties

        public ObservableCollection<ConditionViewModel> Conditions
        {
            get => _conditions;
            set
            {
                _conditions = value;
                FilteredConditions = value != null
                    ? new ObservableCollection<ConditionViewModel>(value)
                    : new ObservableCollection<ConditionViewModel>();
                RaisePropertyChanged();
            }
        }

        public string FilterCsv
        {
            get => _filterCsv;
            set
            {
                _filterCsv = value;
                UpdateFilteredConditionsAsync();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ConditionViewModel> FilteredConditions
        {
            get => _filteredConditions;
            set
            {
                _filteredConditions = value;
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

        private void UpdateFilteredConditionsAsync()
        {
            DispatcherHelper.RunAsync(() =>
            {
                var updatedConditions = _conditions.FilterBy(_filterCsv);
                var conditionsToBeAdded = updatedConditions.Where(ut => !_filteredConditions.Contains(ut)).ToArray();
                var conditionsToBeRemoved = _filteredConditions.Where(ft => !updatedConditions.Contains(ft)).ToArray();

                foreach (var conditionViewModel in conditionsToBeAdded)
                {
                    FilteredConditions.Add(conditionViewModel);
                }

                foreach (var conditionViewModel in conditionsToBeRemoved)
                {
                    FilteredConditions.Remove(conditionViewModel);
                }
            });
        }

        private void InitializeConditions()
        {
            //TODO add all custom ModuleTypes here
            var conditions = new ObservableCollection<ConditionViewModel>
            {
                DayOfWeekViewModel.Default,
                TimePeriodViewModel.Default
            };

            Conditions = conditions;
        }

        #endregion
    }
}