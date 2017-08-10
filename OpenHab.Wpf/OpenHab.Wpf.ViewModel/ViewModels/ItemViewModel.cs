using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Ninject;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Helper;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Constants;
using OpenHAB.NetRestApi.Models;

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

        #endregion

        public ItemViewModel(Item item)
        {
            Type = item.Type.ToEnum(WidgetType.Text);
            Name = item.Name;
            Label = item.Label;
            Category = item.Category;
            Tags = item.Tags.ToViewModels();
            GroupNames = item.GroupNames.ToViewModels();
            Link = item.Link;
            State = item.State;
            TransformedState = item.TransformedState;
            StateDescription = item.StateDescription.ToViewModel();

            _item = item;
        }

        public void Update(Item item)
        {
            Type = item.Type.ToEnum(WidgetType.Text);
            Name = item.Name;
            Label = item.Label;
            Category = item.Category;
            Tags = item.Tags.ToViewModels();
            GroupNames = item.GroupNames.ToViewModels();
            Link = item.Link;
            State = item.State;
            TransformedState = item.TransformedState;
            StateDescription = item.StateDescription.ToViewModel();

            _item = item;
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

        #endregion

        #region Public Methods

        public async void SendCommandAsync(string newState)
        {
            await Task.Run(() =>
                NinjectKernel.StandardKernel.Get<RestContext>().Client.ItemService.SendCommand(_item, _state));
        }

        #endregion

        #region Private Methods

        #endregion
    }
}