using System;
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

        #endregion
        
        public TimeOfDayViewModel()
        {
            Type = "timer.TimeOfDayTrigger";

            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public TimeOfDayViewModel(Trigger trigger) : base(trigger)
        {
            Type = "timer.TimeOfDayTrigger";
            GenerateTimeOfDayFromTrigger(trigger);
        }

        public static TimeOfDayViewModel Default => new TimeOfDayViewModel();

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

        public TimeSpan Time => new TimeSpan(0, Hours, Minutes, Seconds);

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
            Configuration = new { time = Time.ToString() };
        }

        private void GenerateTimeOfDayFromTrigger(Trigger trigger)
        {
            dynamic configuration = trigger.Configuration;
            string time = configuration.time;
            if (time == null) return;
            var timeSpan = TimeSpan.Parse(time);

            Hours = timeSpan.Hours;
            Minutes = timeSpan.Minutes;
            Seconds = timeSpan.Seconds;
        }

        private TimeOfDayViewModel CreateTimeOfDayTrigger()
        {
            return new TimeOfDayViewModel
            {
                Id = Guid.NewGuid().ToString(),

                Hours = _hours,
                Minutes = _minutes,
                Seconds = _seconds,

                Type = "timer.TimeOfDayTrigger",
                Label = Label,
                Description = Description,
                Configuration = Configuration
            };
        }

        #endregion
    }
}