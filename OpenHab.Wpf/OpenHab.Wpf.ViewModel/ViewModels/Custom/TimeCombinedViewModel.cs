using System;
using System.Collections.Generic;
using OpenHab.Wpf.ViewModel.Enums;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels.Custom
{
    /// <summary>
    /// This is a combination of the following ModuleTypes:
    ///     timer.TimeOfDayTrigger
    ///     timer.GenericCronTrigger
    ///     timer.DayOfWeekCondition
    ///     script.ScriptCondition
    /// </summary>
    public class TimeCombinedViewModel : TriggerViewModel
    {
        #region Fields

        private int _startSeconds;
        private int _startMinutes;
        private int _startHours;

        private int _endSeconds;
        private int _endMinutes;
        private int _endHours;

        private TimeDimension _timeDimension = TimeDimension.Day;
        private int _repeatValue = 1;

        private bool _monday;
        private bool _sunday;
        private bool _saturday;
        private bool _friday;
        private bool _thursday;
        private bool _wednesday;
        private bool _tuesday;
        private bool _range;

        #endregion

        public TimeCombinedViewModel(Trigger trigger) : base(trigger)
        {
            switch (trigger.Type ?? trigger.Uid)
            {
                case "timer.TimeOfDayTrigger":
                    //interpret time
                    break;
                case "timer.GenericCronTrigger":
                    //interpret cron
                    break;
            }
        }

        public TimeCombinedViewModel(Condition condition) : base(condition)
        {
            switch (condition.Type ?? condition.Uid)
            {
                case "timer.DayOfWeekCondition":
                    //interpret days
                    break;
            }
        }

        public TimeCombinedViewModel()
        {
            Type = "TimeCombinedViewModel";
            //default
        }

        public static TimeCombinedViewModel Default => new TimeCombinedViewModel {IsTool = true};

        #region Properties

        public int StartHours
        {
            get => _startHours;
            set
            {
                _startHours = value;
                RaisePropertyChanged();
            }
        }

        public int StartMinutes
        {
            get => _startMinutes;
            set
            {
                _startMinutes = value;
                RaisePropertyChanged();
            }
        }

        public int StartSeconds
        {
            get => _startSeconds;
            set
            {
                _startSeconds = value;
                RaisePropertyChanged();
            }
        }

        public int EndHours
        {
            get => _endHours;
            set
            {
                _endHours = value;
                RaisePropertyChanged();
            }
        }

        public int EndMinutes
        {
            get => _endMinutes;
            set
            {
                _endMinutes = value;
                RaisePropertyChanged();
            }
        }

        public int EndSeconds
        {
            get => _endSeconds;
            set
            {
                _endSeconds = value;
                RaisePropertyChanged();
            }
        }

        public bool Range
        {
            get => _range;
            set
            {
                _range = value;
                RaisePropertyChanged();
            }
        }

        public bool Monday
        {
            get => _monday;
            set
            {
                _monday = value;
                RaisePropertyChanged();
            }
        }

        public bool Tuesday
        {
            get => _tuesday;
            set
            {
                _tuesday = value;
                RaisePropertyChanged();
            }
        }

        public bool Wednesday
        {
            get => _wednesday;
            set
            {
                _wednesday = value;
                RaisePropertyChanged();
            }
        }

        public bool Thursday
        {
            get => _thursday;
            set
            {
                _thursday = value;
                RaisePropertyChanged();
            }
        }

        public bool Friday
        {
            get => _friday;
            set
            {
                _friday = value;
                RaisePropertyChanged();
            }
        }

        public bool Saturday
        {
            get => _saturday;
            set
            {
                _saturday = value;
                RaisePropertyChanged();
            }
        }

        public bool Sunday
        {
            get => _sunday;
            set
            {
                _sunday = value;
                RaisePropertyChanged();
            }
        }

        public int RepeatValue
        {
            get => _repeatValue;
            set
            {
                _repeatValue = value;
                RaisePropertyChanged();
            }
        }

        public TimeDimension TimeDimension
        {
            get => _timeDimension;
            set
            {
                _timeDimension = value;
                RaisePropertyChanged();
            }
        }

        public TimeDimension[] TimeDimensions => EnumerableExtensions.EnumToArray<TimeDimension>();

        #endregion

        #region Public Methods

        public TriggerViewModel[] GenerateTriggers()
        {
            var triggers = new List<TriggerViewModel>();
            var timeOfDayTrigger = CreateTimeOfDayTrigger();
            var cronTrigger = CreateCronTrigger();
            if (timeOfDayTrigger != null) triggers.Add(timeOfDayTrigger);
            if (cronTrigger != null) triggers.Add(cronTrigger);
            return triggers.ToArray();
        }

        public ConditionViewModel[] GenerateConditions()
        {
            var conditions = new List<ConditionViewModel>();
            var dayOfWeekCondition = CreateDayOfWeekCondition();
            var timeRangeScriptCondition = CreateTimeRangeScriptCondition();
            if (dayOfWeekCondition != null) conditions.Add(dayOfWeekCondition);
            if (timeRangeScriptCondition != null) conditions.Add(timeRangeScriptCondition);
            return conditions.ToArray();
        }

        #endregion

        #region PrivateMethods

        private TimeOfDayViewModel CreateTimeOfDayTrigger()
        {
            return new TimeOfDayViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Time = new TimeSpan(_startHours, _startMinutes, _startSeconds)
            };
        }

        private TriggerViewModel CreateCronTrigger()
        {
            //TODO implement timer.GenericCronTrigger here
            return null;
        }

        private DayOfWeekViewModel CreateDayOfWeekCondition()
        {
            if (Monday && Tuesday && Wednesday && Thursday && Friday && Saturday && Sunday
                || !Monday && !Tuesday && !Wednesday && !Thursday && !Friday && !Saturday && !Sunday)
            {
                return null;
            }

            return new DayOfWeekViewModel
            {
                Id = Guid.NewGuid().ToString(),

                Monday = _monday,
                Tuesday = _tuesday,
                Wednesday = _wednesday,
                Thursday = _thursday,
                Friday = _friday,
                Saturday = _saturday,
                Sunday = _sunday
            };
        }

        private ConditionViewModel CreateTimeRangeScriptCondition()
        {
            if (!Range) return null;

            var start = new TimeSpan(0, StartHours, StartMinutes, StartSeconds);
            var end = new TimeSpan(0, EndHours, EndMinutes, EndSeconds);
            var ss = start.Add(TimeSpan.FromSeconds(-1));
            var se = end.Add(TimeSpan.FromSeconds(1));

            var script =
                $"((new Date()) >= ((new Date()).setHours({ss.Hours}, {ss.Minutes}, {ss.Seconds}))) || (new Date()) < ((new Date()).setHours({se.Hours}, {se.Minutes}, {se.Seconds}))";

            var configuration = new
            {
                type = "application/javascript",
                script = script
            };

            return new ConditionViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Label = string.Format(Properties.Resources.TimeRangeLabel, start, end),
                Description = string.Format(Properties.Resources.TimeRangeDescription, start, end),
                Configuration = configuration,
                Type = "script.ScriptCondition"
            };
        }

        #endregion
    }
}