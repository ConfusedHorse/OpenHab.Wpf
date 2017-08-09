using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenHab.Wpf.ViewModel.ViewModels;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.Helper
{
    public static class ViewModelExtensions
    {
        public static ObservableCollection<string> ToViewModels(this IEnumerable<string> list)
        {
            return new ObservableCollection<string>(list);
        }

        public static ItemViewModel ToViewModel(this Item item)
        {
            return new ItemViewModel(item);
        }

        public static ObservableCollection<ItemViewModel> ToViewModels(this IEnumerable<Item> items)
        {
            return new ObservableCollection<ItemViewModel>(items.Select(o => o.ToViewModel()));
        }

        public static StateDescriptionViewModel ToViewModel(this StateDescription stateDescription)
        {
            return stateDescription == null ? null : new StateDescriptionViewModel(stateDescription);
        }

        public static OptionViewModel ToViewModel(this Option option)
        {
            return new OptionViewModel(option);
        }

        public static ObservableCollection<OptionViewModel> ToViewModels(this IEnumerable<Option> options)
        {
            return new ObservableCollection<OptionViewModel>(options.Select(o => o.ToViewModel()));
        }
    }
}