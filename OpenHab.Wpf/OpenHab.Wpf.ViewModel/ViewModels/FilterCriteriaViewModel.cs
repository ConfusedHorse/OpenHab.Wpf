using GalaSoft.MvvmLight;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class FilterCriteriaViewModel : ViewModelBase
    {
        #region Fields

        private string _name;
        private string _value;

        #endregion

        public FilterCriteriaViewModel(FilterCriteria filterCriteria)
        {
            Name = filterCriteria.Name;
            Value = filterCriteria.Value;
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

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}