using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Events;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models.Events;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ItemsViewModel : ViewModelBase
    {
        #region Fields

        private readonly RestContext _restContext;
        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        private bool _isEnabled;
        private bool _isBusy;

        #endregion

        public ItemsViewModel(RestContext restContext)
        {
            _restContext = restContext;
            _restContext.ConnectionChanged += RestContextOnConnectionChanged;
        }

        #region Properties

        public ObservableCollection<ItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
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

        private void RestContextOnConnectionChanged(RestContext sender, ServerConnectionChangedEventArgs args)
        {
            IsEnabled = args.ConnectionEstablished;
            if (IsEnabled) Initialize();
            else Terminate();
        }

        private void Initialize()
        {
            RefreshItems();
            InitializeEventHandlers();
        }

        private void Terminate()
        {
            TerminateEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            var eventService = _restContext.Client.EventService;
            Debug.WriteLine($"Initializing Event Handlers for {nameof(ItemsViewModel)}");

            eventService.ItemAdded += OnItemAdded;
            eventService.ItemRemoved += OnItemRemoved;
            //eventService.ItemUpdated += OnItemUpdated; //is now fired by item itself
            //eventService.ItemStateChanged += OnItemStateChanged;
        }

        private void TerminateEventHandlers()
        {
            var eventService = _restContext.Client.EventService;
            Debug.WriteLine($"Terminating Event Handlers for {nameof(ItemsViewModel)}");

            eventService.ItemAdded -= OnItemAdded;
            eventService.ItemRemoved -= OnItemRemoved;
            //eventService.ItemUpdated -= OnItemUpdated; //is now fired by item itself
            //eventService.ItemStateChanged -= OnItemStateChanged;
        }

        private void OnItemAdded(object sender, ItemAddedEvent eventObject)
        {
            var item = eventObject.Item.ToViewModel();
            if (item != null) DispatcherHelper.RunAsync(() => Items.Add(item));
        }

        private void OnItemRemoved(object sender, ItemRemovedEvent eventObject)
        {
            var item = _items.FirstOrDefault(i => i.Name == eventObject.Item.Name);
            if (item != null) DispatcherHelper.RunAsync(() => Items.Remove(item));
        }

        private void OnItemUpdated(object sender, ItemUpdatedEvent eventObject)
        {
            var item = _items.FirstOrDefault(i => i.Name == eventObject.OldItem.Name);
            item?.Update(eventObject.NewItem);
        }

        private void OnItemStateChanged(object sender, ItemStateChangedEvent eventObject)
        {
            var item = _items.FirstOrDefault(i => i.Name == eventObject.Target);
            if (item != null) item.State = eventObject.StateValue;
        }

        private async void RefreshItems()
        {
            if (IsBusy) return;
            IsBusy = true;

            var items = await Task.Run(() => _restContext.Client.ItemService.GetItems());
            var itemInformation = await Task.Run(() =>
            {
                return items.Select(item => _restContext.Client.ItemService.GetItem(item.Name));
            });
            Items = itemInformation.ToViewModels();

            IsBusy = false;
        }

        #endregion
    }
}
