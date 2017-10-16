using System;
using System.Collections.Generic;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels.Custom
{
    /// <summary>
    ///     timer.DayOfWeekCondition
    /// </summary>
    public class DayOfWeekViewModel : ConditionViewModel
    {
        #region Fields

        private bool _monday;
        private bool _sunday;
        private bool _saturday;
        private bool _friday;
        private bool _thursday;
        private bool _wednesday;
        private bool _tuesday;

        #endregion

        public DayOfWeekViewModel()
        {
            Type = "timer.DayOfWeekCondition";

            RefreshInternals();
        }

        public DayOfWeekViewModel(Condition condition) : base(condition)
        {
            Type = "timer.DayOfWeekCondition";
            GenerateTimeOfDayFromCondition(condition);
        }

        public static DayOfWeekViewModel Default => new DayOfWeekViewModel();

        #region Properties

        public bool Monday
        {
            get => _monday;
            set
            {
                _monday = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public bool Tuesday
        {
            get => _tuesday;
            set
            {
                _tuesday = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public bool Wednesday
        {
            get => _wednesday;
            set
            {
                _wednesday = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public bool Thursday
        {
            get => _thursday;
            set
            {
                _thursday = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public bool Friday
        {
            get => _friday;
            set
            {
                _friday = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public bool Saturday
        {
            get => _saturday;
            set
            {
                _saturday = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public bool Sunday
        {
            get => _sunday;
            set
            {
                _sunday = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public List<string> Days
        {
            get
            {
                //if (Monday && Tuesday && Wednesday && Thursday && Friday && Saturday && Sunday
                //    || !Monday && !Tuesday && !Wednesday && !Thursday && !Friday && !Saturday && !Sunday)
                //{
                //    return null;
                //}

                var listOfDays = new List<string>();
                if (Monday) listOfDays.Add("MON");
                if (Tuesday) listOfDays.Add("TUE");
                if (Wednesday) listOfDays.Add("WED");
                if (Thursday) listOfDays.Add("THU");
                if (Friday) listOfDays.Add("FRI");
                if (Saturday) listOfDays.Add("SAT");
                if (Sunday) listOfDays.Add("SUN");

                return listOfDays;
            }
        }

        #endregion

        #region Public Methods

        public DayOfWeekViewModel GenerateCondition()
        {
            return CreateDayOfWeekCondition();
        }

        #endregion

        #region PrivateMethods

        private void RefreshInternals()
        {
            Label = string.Format(Properties.Resources.DaysOfWeekLabel, Days.Count);
            Description = $"{Properties.Resources.DaysOfWeekDescription}{string.Join(", ", Days)}";
            Configuration = new { days = Days.ToArray() };
        }

        private void GenerateTimeOfDayFromCondition(Condition condition)
        {
            dynamic configuration = condition.Configuration;
            var days = configuration.days.ToObject<List<string>>();
            if (days == null) return;

            foreach (var day in days)
            {
                switch (day)
                {
                    case "MON": Monday = true; break;
                    case "TUE": Tuesday = true; break;
                    case "WED": Wednesday = true; break;
                    case "THU": Thursday = true; break;
                    case "FRI": Friday = true; break;
                    case "SAT": Saturday = true; break;
                    case "SUN": Sunday = true; break;
                }
            }
        }

        private DayOfWeekViewModel CreateDayOfWeekCondition()
        {
            var configuration = new
            {
                days = Days.ToArray()
            };

            return new DayOfWeekViewModel
            {
                Id = Guid.NewGuid().ToString(),

                Monday = _monday,
                Tuesday = _tuesday,
                Wednesday = _wednesday,
                Thursday = _thursday,
                Friday = _friday,
                Saturday = _saturday,
                Sunday = _sunday,

                Type = "timer.DayOfWeekCondition",
                Label = Label,
                Description = Description,
                Configuration = configuration
            };
        }

        #endregion
    }
}