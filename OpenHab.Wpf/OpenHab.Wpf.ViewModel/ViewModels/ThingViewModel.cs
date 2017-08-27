using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Ninject;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;
using OpenHAB.NetRestApi.Models.Events;

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
            InitializeEventHandlers();

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

        public void Update(Thing thing)
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

        #endregion

        #region Private Methods

        private async void RefreshLinkedItemsAsync()
        {
            if (_channels.IsNullOrEmpty()) return;
            LinkedItems = await Task.Run(() => _thing.GetLinkedItems()?.ToViewModels());
        }

        private void InitializeEventHandlers()
        {
            if (_thing == null) return;
            _thing.Updated += ThingOnUpdated;
            _thing.ItemChannelLinkAdded += ThingOnItemChannelLinkAdded;
            _thing.ItemChannelLinkRemoved += ThingOnItemChannelLinkRemoved;

            _thing.InitializeEvents();
        }

        private void ThingOnUpdated(object sender, ThingUpdatedEvent eventObject)
        {
            Update(eventObject.NewThing);
        }

        private void ThingOnItemChannelLinkAdded(object sender, ItemChannelLinkAddedEvent eventObject)
        {
            if (LinkedItems.IsNullOrEmpty()) return;

            var client = NinjectKernel.StandardKernel.Get<RestContext>().Client;
            var item = client.ItemService.GetItem(eventObject.ItemName);
            DispatcherHelper.RunAsync(() => LinkedItems.Add(item.ToViewModel()));
        }

        private void ThingOnItemChannelLinkRemoved(object sender, ItemChannelLinkRemovedEvent eventObject)
        {
            if (LinkedItems.IsNullOrEmpty()) return;
            
            var item = _linkedItems.FirstOrDefault(li => li.Name == eventObject.ItemName);
            if (item == null) return;
            DispatcherHelper.RunAsync(() => LinkedItems.Remove(item));
        }

        #endregion
    }
}