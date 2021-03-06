﻿using GalaSoft.MvvmLight;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class OptionViewModel : ViewModelBase
    {
        #region Fields

        private string _label;
        private string _value;

        #endregion

        public OptionViewModel(Option option)
        {
            Label = option.Label;
            Value = option.Value;
        }

        #region Properties

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

        #endregion
    }
}