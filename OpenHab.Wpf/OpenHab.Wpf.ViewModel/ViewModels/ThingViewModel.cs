using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ThingViewModel : ViewModelBase
    {
        #region Fields

        private string _bridgeUid;
        private ObservableCollection<ChannelViewModel> _channels;
        private ObservableCollection<ItemViewModel> _linkedItems;
        private object _configuration;
        private bool _editable;
        private FirmwareViewModel _firmwareStatus;
        private string _label;
        private string _location;
        private object _properties;
        private StatusInfoViewModel _statusInfo;
        private string _thingTypeUid;
        private string _uid;

        private Thing _thing;

        #endregion

        public ThingViewModel(Thing thing)
        {
            Label = thing.Label;
            BridgeUid = thing.BridgeUid;
            Configuration = thing.Configuration;
            Properties = thing.Properties;
            Uid = thing.Uid;
            ThingTypeUid = thing.ThingTypeUid;
            Channels = thing.Channels?.ToViewModels();
            Location = thing.Location;
            StatusInfo = thing.StatusInfo?.ToViewModel();
            FirmwareStatus = thing.FirmwareStatus?.ToViewModel();
            Editable = thing.Editable;

            _thing = thing;

            RefreshLinkedItemsAsync();
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

        public string BridgeUid
        {
            get => _bridgeUid;
            set
            {
                _bridgeUid = value;
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

        public object Properties
        {
            get => _properties;
            set
            {
                _properties = value;
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

        public string ThingTypeUid
        {
            get => _thingTypeUid;
            set
            {
                _thingTypeUid = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ChannelViewModel> Channels
        {
            get => _channels;
            set
            {
                _channels = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ItemViewModel> LinkedItems
        {
            get => _linkedItems;
            set
            {
                _linkedItems = value;
                RaisePropertyChanged();
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                RaisePropertyChanged();
            }
        }

        public StatusInfoViewModel StatusInfo
        {
            get => _statusInfo;
            set
            {
                _statusInfo = value;
                RaisePropertyChanged();
            }
        }

        public FirmwareViewModel FirmwareStatus
        {
            get => _firmwareStatus;
            set
            {
                _firmwareStatus = value;
                RaisePropertyChanged();
            }
        }

        public bool Editable
        {
            get => _editable;
            set
            {
                _editable = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private async void RefreshLinkedItemsAsync()
        {
            if (_channels.IsNullOrEmpty()) return;
            LinkedItems = await Task.Run(() => _thing.GetLinkedItems().ToViewModels());
        }

        #endregion
    }
}