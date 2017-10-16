using System;
using System.Collections.Generic;
using System.Dynamic;
using Ninject;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.Enums;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels.Custom
{
    /// <summary>
    ///     script.ScriptCondition
    /// </summary>
    public class TimePeriodViewModel : ConditionViewModel
    {
        #region Fields

        private int _startSeconds;
        private int _startMinutes;
        private int _startHours;
        private TimeSpan _startTime;
        private TimeSpan _startTimeOnLoaded;

        private int _endSeconds;
        private int _endMinutes;
        private int _endHours;
        private TimeSpan _endTime;
        private TimeSpan _endTimeOnLoaded;

        private readonly bool _isLoaded;
        private string _scriptOnLoaded;

        #endregion

        public TimePeriodViewModel()
        {
            Type = "TimePeriodViewModel";

            RefreshInternals();
            _startTimeOnLoaded = StartTime;
            _endTime = EndTime;

            dynamic configuration = Configuration;
            _scriptOnLoaded = configuration.script;

            _isLoaded = true;
        }

        public TimePeriodViewModel(Condition condition) : base(condition)
        {
            Type = "script.ScriptCondition";

            GenerateTimePeriodFromCondition(condition);
            _isLoaded = true;
        }

        public static TimePeriodViewModel Default => new TimePeriodViewModel { IsTool = true };

        #region Properties

        public int StartHours
        {
            get => _startHours;
            set
            {
                if (_startHours == value) return;
                _startHours = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public int StartMinutes
        {
            get => _startMinutes;
            set
            {
                if (_startMinutes == value) return;
                _startMinutes = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public int StartSeconds
        {
            get => _startSeconds;
            set
            {
                if (_startSeconds == value) return;
                _startSeconds = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public int EndHours
        {
            get => _endHours;
            set
            {
                if (_endHours == value) return;
                _endHours = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public int EndMinutes
        {
            get => _endMinutes;
            set
            {
                if (_endMinutes == value) return;
                _endMinutes = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public int EndSeconds
        {
            get => _endSeconds;
            set
            {
                if (_endSeconds == value) return;
                _endSeconds = value;
                RaisePropertyChanged();
                RefreshInternals();
            }
        }

        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime == value) return;
                _startTime = value;
                if (!_isLoaded) return;
                StartHours = _startTime.Hours;
                StartMinutes = _startTime.Minutes;
                StartSeconds = _startTime.Seconds;
                RaisePropertyChanged();
            }
        }

        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                if (_endTime == value) return;
                _endTime = value;
                if (!_isLoaded) return;
                EndHours = _endTime.Hours;
                EndMinutes = _endTime.Minutes;
                EndSeconds = _endTime.Seconds;
                RaisePropertyChanged();
            }
        }

        public TimeDimension[] TimeDimensions => EnumerableExtensions.EnumToArray<TimeDimension>();

        #endregion

        #region Public Methods

        public TimePeriodViewModel GenerateCondition()
        {
            return CreateTimePeriodCondition();
        }

        #endregion

        #region PrivateMethods

        private void RefreshInternals()
        {
            Label = string.Format(Properties.Resources.TimeRangeLabel, StartTime, EndTime);
            Description = string.Format(Properties.Resources.TimeRangeDescription, StartTime, EndTime);

            var ss = StartTime.Add(TimeSpan.FromSeconds(-1));
            var se = EndTime.Add(TimeSpan.FromSeconds(1));

            var script =
                $"((new Date()) >= ((new Date()).setHours({ss.Hours}, {ss.Minutes}, {ss.Seconds}))) || (new Date()) < ((new Date()).setHours({se.Hours}, {se.Minutes}, {se.Seconds}))";

            dynamic configuration = new ExpandoObject();
            configuration.type = "application/javascript";
            configuration.script = script;

            Configuration = configuration;

            if (!IsTool && _isLoaded)
            {
                var ruleViewModel = NinjectKernel.StandardKernel.Get<RulesViewModel>().CurrentRule;
                if (ruleViewModel != null)
                    ruleViewModel.UnsavedChanges =
                        _scriptOnLoaded == script;
                //_startTimeOnLoaded != _startTime || _endTimeOnLoaded != _endTime;
            }
        }

        private void GenerateTimePeriodFromCondition(Condition condition)
        {
            // TODO read from tag or whatever
            // don't forget to update CreateTimePeriodCondition
            throw new NotImplementedException();

            //_startTimeOnLoaded = startTime;
            //_endTimeOnLoaded = endTime;
            //_scriptOnLoaded = script;
        }

        private TimePeriodViewModel CreateTimePeriodCondition()
        {
            return new TimePeriodViewModel
            {
                Id = Guid.NewGuid().ToString(),

                StartHours = _startHours,
                StartMinutes = _startMinutes,
                StartSeconds = _startSeconds,
                StartTime = new TimeSpan(0, _startHours, _startMinutes, _startSeconds),

                EndHours = _endHours,
                EndMinutes = _endMinutes,
                EndSeconds = _endSeconds,
                EndTime = new TimeSpan(0, _endHours, _endMinutes, _endSeconds),

                // replace "script.ScriptCondition" with "TimePeriodViewModel" when
                // GenerateTimePeriodFromCondition is implemented
                Type = "script.ScriptCondition",
                Label = string.Format(Properties.Resources.TimeRangeLabel, StartTime, EndTime),
                Description = string.Format(Properties.Resources.TimeRangeDescription, StartTime, EndTime),
                Configuration = Configuration
            };
        }

        #endregion
    }
}