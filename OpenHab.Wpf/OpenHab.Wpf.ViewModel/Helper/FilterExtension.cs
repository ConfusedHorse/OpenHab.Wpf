using System.Collections.ObjectModel;
using System.Linq;
using OpenHab.Wpf.CrossCutting.Helpers;
using OpenHab.Wpf.ViewModel.ViewModels;

namespace OpenHab.Wpf.ViewModel.Helper
{
    public static class FilterExtension
    {
        public static ObservableCollection<ThingViewModel> FilterBy(this ObservableCollection<ThingViewModel> self,
            string filterCsv)
        {
            var filteredThings = string.IsNullOrEmpty(filterCsv)
                ? self
                : self.Where(t => CreateBrowseValue(t).Search(filterCsv));

            return new ObservableCollection<ThingViewModel>(filteredThings);
        }

        private static string CreateBrowseValue(ThingViewModel t)
        {
            return $"{t.Uid}{t.ThingTypeUid}{t.Label}{t.Location}";
        }
    }
}