using System;
using Ninject;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels.Custom
{
    /// <summary>
    ///     timer.TimeOfDayTrigger
    /// </summary>
    public class TimeOfDayViewModel : TriggerViewModel
    {
        #region Fields

        private int _seconds;
        private int _minutes;
        private int _hours;
        private TimeSpan _time;

        private readonly bool _isLoaded;

        #endregion
        
        public TimeOfDayViewModel()
        {
            Type = "timer.TimeOfDayTrigger";

            RefreshInternals();
            _isLoaded = true;
        }

        public TimeOfDayViewModel(Trigger trigger) : base(trigger)
        {
            Type = "timer.TimeOfDayTrigger";

            GenerateTimeOfDayFromTrigger(trigger);
            _isLoaded = true;
        }

        public static TimeOfDayViewModel Default => new TimeOfDayViewModel {IsTool = true};

        #region Properties

        public int Hours
        {
            get => _hours;
            set
            {
                _hours = value;                
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public TimeSpan Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                if (!_isLoaded) return;
                Hours = _time.Hours;
                Minutes = _time.Minutes;
                Seconds = _time.Seconds;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        public TimeOfDayViewModel GenerateTrigger()
        {
            return CreateTimeOfDayTrigger();
        }

        #endregion

        #region PrivateMethods

        private void RefreshInternals()
        {
            Label = string.Format(Properties.Resources.TimeOfDayLabel, Time);
            Description = string.Format(Properties.Resources.TimeOfDayDescription, Time);
            Configuration = new {time = Time.ToString()};

            if(!IsTool && _isLoaded)
            {
                var ruleViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
                if (ruleViewModel != null)
                    ruleViewModel.UnsavedChanges = true;
            }
        }

        private void GenerateTimeOfDayFromTrigger(Trigger trigger)
        {
            dynamic configuration = trigger.Configuration;
            string time = configuration.time;
            if (time == null) return;
            var timeSpan = TimeSpan.Parse(time);

            Time = timeSpan;
        }

        private TimeOfDayViewModel CreateTimeOfDayTrigger()
        {
            return new TimeOfDayViewModel
            {
                Id = Guid.NewGuid().ToString(),

                Hours = _hours,
                Minutes = _minutes,
                Seconds = _seconds,
                Time = new TimeSpan(0, _hours, _minutes, _seconds),

                Type = "timer.TimeOfDayTrigger",
                Label = Label,
                Description = Description,
                Configuration = Configuration
            };
        }

        #endregion
    }
}