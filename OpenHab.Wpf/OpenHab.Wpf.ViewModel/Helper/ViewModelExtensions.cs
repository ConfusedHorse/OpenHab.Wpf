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

        public static ThingViewModel ToViewModel(this Thing thing)
        {
            return new ThingViewModel(thing);
        }

        public static ObservableCollection<ThingViewModel> ToViewModels(this IEnumerable<Thing> things)
        {
            return new ObservableCollection<ThingViewModel>(things.Select(o => o.ToViewModel()));
        }

        public static OptionViewModel ToViewModel(this Option option)
        {
            return new OptionViewModel(option);
        }

        public static ObservableCollection<OptionViewModel> ToViewModels(this IEnumerable<Option> options)
        {
            return new ObservableCollection<OptionViewModel>(options.Select(o => o.ToViewModel()));
        }

        public static ChannelViewModel ToViewModel(this Channel channel)
        {
            return new ChannelViewModel(channel);
        }

        public static ObservableCollection<ChannelViewModel> ToViewModels(this IEnumerable<Channel> channels)
        {
            return new ObservableCollection<ChannelViewModel>(channels.Select(o => o.ToViewModel()));
        }

        public static StatusInfoViewModel ToViewModel(this StatusInfo statusInfo)
        {
            return new StatusInfoViewModel(statusInfo);
        }

        public static FirmwareViewModel ToViewModel(this Firmware firmware)
        {
            return new FirmwareViewModel(firmware);
        }
    }
}