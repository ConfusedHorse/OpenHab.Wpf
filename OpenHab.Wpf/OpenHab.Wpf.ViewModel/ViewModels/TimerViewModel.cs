using OpenHab.Wpf.ViewModel.Enums;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    /// <summary>
    /// This is a combination of the following ModuleTypes:
    ///     timer.TimeOfDayTrigger
    ///     timer.GenericCronTrigger
    ///     timer.DayOfWeekCondition
    /// </summary>
    public class TimerViewModel : TriggerViewModel
    {
        private int _startSeconds;
        private int _startMinutes;
        private int _startHours;

        private TimeDimension _timeDimension = TimeDimension.Day;
        private int _repeatValue = 1;

        private bool _monday;
        private bool _sunday;
        private bool _saturday;
        private bool _friday;
        private bool _thursday;
        private bool _wednesday;
        private bool _tuesday;
        private int _endSeconds;
        private int _endMinutes;
        private int _endHours;

        #region Fields



        #endregion

        public TimerViewModel(Trigger trigger) : base(trigger)
        {
            switch (trigger.Type ?? trigger.Uid)
            {
                case "timer.TimeOfDayTrigger":
                    //interpret days
                    break;
                case "timer.GenericCronTrigger":
                    //interpret cron
                    break;
            }
        }

        public TimerViewModel(Condition condition) : base(condition)
        {
            switch (condition.Type ?? condition.Uid)
            {
                case "timer.DayOfWeekCondition":
                    //interpret days
                    break;
            }
        }

        public TimerViewModel(ItemViewModel itemViewModel) : base(itemViewModel)
        {
            //not supported
        }

        public TimerViewModel()
        {
            Type = "TimerViewModel";
            //default
        }

        public static TimerViewModel Default => new TimerViewModel();

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
    }
}