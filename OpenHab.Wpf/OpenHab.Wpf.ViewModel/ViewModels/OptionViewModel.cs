using GalaSoft.MvvmLight;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class OptionViewModel : ViewModelBase
    {
        private string _label;
        private string _value;

        public OptionViewModel(Option option)
        {
            Label = option.Label;
            Value = option.Value;
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

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }
}