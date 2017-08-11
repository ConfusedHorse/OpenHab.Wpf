using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ChannelViewModel : ViewModelBase
    {
        private bool _advanced;
        private string _category;
        private string _description;
        private string _id;
        private string _itemType;
        private string _kind;
        private string _label;
        private object _properties;
        private StateDescriptionViewModel _stateDescription;
        private ObservableCollection<string> _tags;
        private object _thingConfiguration;
        private string _typeUid;
        private string _uid;

        private Channel _channel;

        public ChannelViewModel(Channel channel)
        {
            Uid = channel.Uid;
            Id = channel.Id;
            TypeUid = channel.TypeUid;
            ItemType = channel.ItemType;
            Kind = channel.Kind;
            Label = channel.Label;
            Description = channel.Description;
            Tags = channel.Tags.ToViewModels();
            Properties = channel.Properties;
            Category = channel.Category;
            StateDescription = channel.StateDescription.ToViewModel();
            Advanced = channel.Advanced;
            ThingConfiguration = channel.ThingConfiguration;

            _channel = channel;
        }

        #region Fields

        #endregion

        #region Properties

        public string Uid
        {
            get => _uid;
            set
            {
                _uid = value;
                RaisePropertyChanged();
            }
        }

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        public string TypeUid
        {
            get => _typeUid;
            set
            {
                _typeUid = value;
                RaisePropertyChanged();
            }
        }

        public string ItemType
        {
            get => _itemType;
            set
            {
                _itemType = value;
                RaisePropertyChanged();
            }
        }

        public string Kind
        {
            get => _kind;
            set
            {
                _kind = value;
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

        public ObservableCollection<string> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                RaisePropertyChanged();
            }
        }

        public object Properties
        {
            get => _properties;
            set
            {
                _properties = value;
                RaisePropertyChanged();
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                RaisePropertyChanged();
            }
        }

        public StateDescriptionViewModel StateDescription
        {
            get => _stateDescription;
            set
            {
                _stateDescription = value;
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

        public object ThingConfiguration
        {
            get => _thingConfiguration;
            set
            {
                _thingConfiguration = value;
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