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
            var channelsBrowseValue = string.Join(string.Empty, t.LinkedItems.Select(CreateBrowseValue));
            return $"{t.Uid}{t.ThingTypeUid}{t.Label}{t.Location}{channelsBrowseValue}";
        }

        private static string CreateBrowseValue(ItemViewModel i)
        {
            var tagsBrowseValue = i.Tags != null ? string.Join(string.Empty, i.Tags) : string.Empty;
            return $"{i.Label}{i.Name}{i.Category}{tagsBrowseValue}";
        }

        public static ObservableCollection<RuleViewModel> FilterBy(this ObservableCollection<RuleViewModel> self,
            string filterCsv)
        {
            var filteredRules = string.IsNullOrEmpty(filterCsv)
                ? self
                : self.Where(r => CreateBrowseValue(r).Search(filterCsv));

            return new ObservableCollection<RuleViewModel>(filteredRules);
        }

        public static bool FilterBy(this RuleViewModel self, string filterCsv)
        {
            return CreateBrowseValue(self).Search(filterCsv);
        }

        private static string CreateBrowseValue(RuleViewModel r)
        {
            var actionsBrowseValue = string.Join(string.Empty, r.Actions.Select(CreateBrowseValue));
            var triggersBrowseValue = string.Join(string.Empty, r.Triggers.Select(CreateBrowseValue));
            var conditionsBrowseValue = string.Join(string.Empty, r.Conditions.Select(CreateBrowseValue));
            var tagsBrowseValue = r.Tags != null ? string.Join(string.Empty, r.Tags) : string.Empty;
            return
                $"{r.Uid}{r.Name}{r.Description}{actionsBrowseValue}{triggersBrowseValue}{conditionsBrowseValue}{tagsBrowseValue}";
        }

        private static string CreateBrowseValue(ActionViewModel a)
        {
            var tagsBrowseValue = a.Tags != null ? string.Join(string.Empty, a.Tags) : string.Empty;
            return $"{a.Label}{a.Label}{a.Type}{a.Uid}{a.Description}{tagsBrowseValue}";
        }

        private static string CreateBrowseValue(TriggerViewModel t)
        {
            var tagsBrowseValue = t.Tags != null ? string.Join(string.Empty, t.Tags) : string.Empty;
            return $"{t.Label}{t.Label}{t.Type}{t.Uid}{t.Description}{tagsBrowseValue}";
        }

        private static string CreateBrowseValue(ConditionViewModel c)
        {
            var tagsBrowseValue = c.Tags != null ? string.Join(string.Empty, c.Tags) : string.Empty;
            return $"{c.Label}{c.Label}{c.Type}{c.Uid}{c.Description}{tagsBrowseValue}";
        }

        public static ObservableCollection<TriggerViewModel> FilterBy(this ObservableCollection<TriggerViewModel> self,
            string filterCsv)
        {
            var filteredTriggers = string.IsNullOrEmpty(filterCsv)
                ? self
                : self.Where(t => CreateBrowseValue(t).Search(filterCsv));

            return new ObservableCollection<TriggerViewModel>(filteredTriggers);
        }
    }
}