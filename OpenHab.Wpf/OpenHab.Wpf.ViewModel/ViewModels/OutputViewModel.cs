using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class OutputViewModel : ViewModelBase
    {
        #region Fields

        private string _reference;
        private string _description;
        private string _label;
        private ObservableCollection<string> _tags;
        private string _type;
        private string _name;

        private Output _output;

        #endregion

        public OutputViewModel(Output output)
        {
            Name = output.Name;
            Type = output.Type;
            Tags = output.Tags?.ToViewModels();
            Label = output.Label;
            Description = output.Description;
            Reference = output.Reference;

            _output = output;
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

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
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

        public string Reference
        {
            get => _reference;
            set
            {
                _reference = value;
                RaisePropertyChanged();
            }
        }
    }
}