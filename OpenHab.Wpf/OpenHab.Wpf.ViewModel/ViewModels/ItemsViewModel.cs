using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Events;
using OpenHab.Wpf.ViewModel.Helper;

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
            if (IsEnabled) RefreshItems();
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
