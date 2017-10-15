using System;
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
    public class ThingsViewModel : ViewModelBase
    {
        #region Fields

        private readonly RestContext _restContext;
        private ObservableCollection<ThingViewModel> _things = new ObservableCollection<ThingViewModel>();
        private bool _isEnabled;
        private bool _isBusy;
        private string _filterCsv;
        private ObservableCollection<ThingViewModel> _filteredThings = new ObservableCollection<ThingViewModel>();

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
                FilteredThings = value != null
                    ? new ObservableCollection<ThingViewModel>(value)
                    : new ObservableCollection<ThingViewModel>();
                RaisePropertyChanged();
            }
        }

        public string FilterCsv
        {
            get => _filterCsv;
            set
            {
                _filterCsv = value;
                UpdateFilteredThingsAsync();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ThingViewModel> FilteredThings
        {
            get => _filteredThings;
            set
            {
                _filteredThings = value;
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
                RaisePropertyChanged(() => Idle);
            }
        }
        public bool Idle => !IsBusy;

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void UpdateFilteredThingsAsync()
        {
            DispatcherHelper.RunAsync(() =>
            {
                var updatedThings = _things.FilterBy(_filterCsv);
                var thingsToBeAdded = updatedThings.Where(ut => !_filteredThings.Contains(ut)).ToArray();
                var thingsToBeRemoved = _filteredThings.Where(ft => !updatedThings.Contains(ft)).ToArray();

                foreach (var thingViewModel in thingsToBeAdded)
                {
                    FilteredThings.Add(thingViewModel);
                }

                foreach (var thingViewModel in thingsToBeRemoved)
                {
                    FilteredThings.Remove(thingViewModel);
                }
            });
        }

        private void RestContextOnConnectionChanged(RestContext sender, ServerConnectionChangedEventArgs args)
        {
            IsEnabled = args.ConnectionEstablished;
            if (IsEnabled) Initialize();
            else Terminate();
        }

        private void Initialize()
        {
            RefreshThingsAsync();
            InitializeEventHandlers();
        }

        private void Terminate()
        {
            TerminateEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            var eventService = _restContext.Client.EventService;
            eventService.ThingAdded += EventServiceOnThingAdded;
            eventService.ThingRemoved += EventServiceOnThingRemoved;
            //TODO implement event handlers
            Debug.WriteLine($"Initializing Event Handlers for {nameof(ThingsViewModel)}");
        }

        private void TerminateEventHandlers()
        {
            var eventService = _restContext?.Client?.EventService;
            if (eventService == null) return;
            Debug.WriteLine($"Terminating Event Handlers for {nameof(ThingsViewModel)}");
        }

        private void EventServiceOnThingAdded(object sender, ThingAddedEvent eventObject)
        {
            //if (Things.IsNullOrEmpty()) return;

            var thing = eventObject.Thing.ToViewModel();
            DispatcherHelper.RunAsync(() =>
            {
                Things.Add(thing);
                if (thing.FilterBy(_filterCsv))
                    FilteredThings.Add(thing);
            });
        }

        private void EventServiceOnThingRemoved(object sender, ThingRemovedEvent eventObject)
        {
            if (Things.IsNullOrEmpty()) return;

            var thing = _things.FirstOrDefault(r => r.Uid == eventObject.Thing.Uid);
            if (thing == null) return;
            DispatcherHelper.RunAsync(() =>
            {
                Things.Remove(thing);
                FilteredThings.Remove(thing);
            });
        }

        private async void RefreshThingsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            var things = await Task.Run(() => _restContext.Client.ThingService.GetThings());
            var usefulThings = things?.Where(t => t.Channels.Any()).ToViewModels();
            
            if (usefulThings != null) Things = usefulThings;

            IsBusy = false;
        }

        #endregion
    }
}
