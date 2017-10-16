using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Helpers;
using OpenHab.Wpf.ViewModel.Enums;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Constants;
using OpenHAB.NetRestApi.Models;
using OpenHAB.NetRestApi.Models.Events;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        #region Fields
        
        private StateDescriptionViewModel _stateDescription;
        private string _transformedState;
        private string _state;
        private string _link;
        private ObservableCollection<string> _groupNames;
        private ObservableCollection<string> _tags;
        private string _category;
        private string _label;
        private string _name;
        private WidgetType _type;
        
        private Item _item;
        private Synchronize _direction;
        private bool _isProxy;

        #endregion

        public ItemViewModel(Item item, Synchronize direction = Synchronize.Twoway)
        {
            Type = item.Type.ToEnum(WidgetType.Text);
            Name = item.Name;
            Label = item.Label;
            Category = item.Category;
            Tags = item.Tags?.ToViewModels();
            GroupNames = item.GroupNames?.ToViewModels();
            Link = item.Link;
            State = item.State;
            TransformedState = item.TransformedState;
            StateDescription = item.StateDescription?.ToViewModel();

            _item = item;
            Direction = direction;
            InitializeEventHandlers();
        }

        internal ItemViewModel(ItemViewModel itemViewModel, Synchronize direction = Synchronize.Twoway)
        {
            Type = itemViewModel.Type;
            Name = itemViewModel.Name;
            Label = itemViewModel.Label;
            Category = itemViewModel.Category;
            Tags = itemViewModel.Tags?.ToViewModels();
            GroupNames = itemViewModel.GroupNames?.ToViewModels();
            Link = itemViewModel.Link;
            State = itemViewModel.State;
            TransformedState = itemViewModel.TransformedState;
            StateDescription = itemViewModel.StateDescription;

            _item = itemViewModel._item;
            Direction = direction;
            InitializeEventHandlers();
        }

        #region Properties
        
        public WidgetType Type
        {
            get => _type;
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
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

        public string Label
        {
            get => _label;
            set
            {
                _label = value;
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

        public ObservableCollection<string> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> GroupNames
        {
            get => _groupNames;
            set
            {
                _groupNames = value;
                RaisePropertyChanged();
            }
        }

        public string Link
        {
            get => _link;
            set
            {
                _link = value;
                RaisePropertyChanged();
            }
        }

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }

        public string FormattedState
        {
            get
            {
                var format = StateDescription?.Pattern;
                return format != null ? string.Format(format, _state) : _state;
            }
        }

        public string TransformedState
        {
            get => _transformedState;
            set
            {
                _transformedState = value;
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

        public Synchronize Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        public async void SendCommandAsync(string newState)
        {
            State = newState;
            if (Direction == Synchronize.OneWay || Direction == Synchronize.Disabled) return;
            await Task.Run(() => _item.SendCommand(_state));
        }

        public async void ForceStateCommand()
        {
            await Task.Run(() => _item.SendCommand(_state));
        }

        public void Update(Item item)
        {
            Type = item.Type.ToEnum(WidgetType.Text);
            Name = item.Name;
            Label = item.Label;
            Category = item.Category;
            Tags = item.Tags?.ToViewModels();
            GroupNames = item.GroupNames?.ToViewModels();
            Link = item.Link;
            State = item.State;
            TransformedState = item.TransformedState;
            StateDescription = item.StateDescription?.ToViewModel();

            _item = item;
        }

        public ItemViewModel GetProxy(Synchronize direction = Synchronize.Twoway)
        {
            return new ItemViewModel(this, direction);
        }

        #endregion

        #region Private Methods

        private void InitializeEventHandlers()
        {
            if (_item == null) return;

            _item.Updated += OnUpdated;
            _item.StateChanged += OnStateChanged;

            _item.InitializeEvents();
        }

        private void OnUpdated(object sender, ItemUpdatedEvent eventObject)
        {
            if (Direction == Synchronize.OneWayToSource || Direction == Synchronize.Disabled) return;
            Update(eventObject.NewItem);
        }

        private void OnStateChanged(object sender, ItemStateChangedEvent eventObject)
        {
            if (Direction == Synchronize.OneWayToSource || Direction == Synchronize.Disabled) return;
            State = eventObject.StateValue;
        }

        #endregion
    }
}