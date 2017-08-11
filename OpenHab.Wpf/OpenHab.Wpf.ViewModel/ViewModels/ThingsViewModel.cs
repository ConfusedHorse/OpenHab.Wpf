using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Events;
using OpenHab.Wpf.ViewModel.Helper;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ThingsViewModel : ViewModelBase
    {
        #region Fields

        private readonly RestContext _restContext;
        private ObservableCollection<ThingViewModel> _things = new ObservableCollection<ThingViewModel>();
        private bool _isEnabled;
        private bool _isBusy;

        #endregion

        public ThingsViewModel(RestContext restContext)
        {
            _restContext = restContext;
            _restContext.ConnectionChanged += RestContextOnConnectionChanged;
        }

        #region Properties

        public ObservableCollection<ThingViewModel> Things
        {
            get => _things;
            set
            {
                _things = value;
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
            RefreshThings();
            InitializeEventHandlers();
        }

        private void Terminate()
        {
            TerminateEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            var eventService = _restContext.Client.EventService;
            Debug.WriteLine($"Initializing Event Handlers for {nameof(ThingsViewModel)}");
            
        }

        private void TerminateEventHandlers()
        {
            var eventService = _restContext.Client.EventService;
            Debug.WriteLine($"Terminating Event Handlers for {nameof(ThingsViewModel)}");
            
        }
        
        private async void RefreshThings()
        {
            if (IsBusy) return;
            IsBusy = true;

            var things = await Task.Run(() => _restContext.Client.ThingService.GetThings());
            var usefulThings = things.Where(t => t.Channels.Any());
            
            Things = usefulThings.ToViewModels();

            IsBusy = false;
        }

        #endregion
    }
}
