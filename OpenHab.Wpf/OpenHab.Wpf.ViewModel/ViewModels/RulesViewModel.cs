using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Ninject;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Events;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.ViewModel.Helper;
using OpenHAB.NetRestApi.Models.Events;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class RulesViewModel : ViewModelBase
    {
        #region Fields

        private readonly RestContext _restContext;
        private ObservableCollection<RuleViewModel> _rules = new ObservableCollection<RuleViewModel>();
        private bool _isEnabled;
        private bool _isBusy;
        private string _filterCsv;
        private ObservableCollection<RuleViewModel> _filteredRules = new ObservableCollection<RuleViewModel>();
        private RuleViewModel _currentRule;

        #endregion

        public RulesViewModel(RestContext restContext)
        {
            _restContext = restContext;
            _restContext.ConnectionChanged += RestContextOnConnectionChanged;
        }

        #region Properties

        public ObservableCollection<RuleViewModel> Rules
        {
            get => _rules;
            set
            {
                _rules = value;
                FilteredRules = new ObservableCollection<RuleViewModel>(value);
                RaisePropertyChanged();
            }
        }

        public string FilterCsv
        {
            get => _filterCsv;
            set
            {
                _filterCsv = value;

                NinjectKernel.StandardKernel.Get<ThingsViewModel>().FilterCsv = value;

                UpdateFilteredThingsAsync();
                RaisePropertyChanged();
            }
        }

        private void UpdateFilteredThingsAsync()
        {
            DispatcherHelper.RunAsync(() =>
            {
                var updateRules = _rules.FilterBy(_filterCsv);
                var rulesToBeAdded = updateRules.Where(ut => !_filteredRules.Contains(ut)).ToArray();
                var rulesToBeRemoved = _filteredRules.Where(ft => !updateRules.Contains(ft)).ToArray();

                foreach (var ruleViewModel in rulesToBeAdded)
                {
                    FilteredRules.Add(ruleViewModel);
                }

                foreach (var ruleViewModel in rulesToBeRemoved)
                {
                    FilteredRules.Remove(ruleViewModel);
                }
            });
        }

        public ObservableCollection<RuleViewModel> FilteredRules
        {
            get => _filteredRules;
            set
            {
                _filteredRules = value;
                RaisePropertyChanged();
            }
        }

        public RuleViewModel CurrentRule
        {
            get => _currentRule;
            set
            {
                _currentRule = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void RestContextOnConnectionChanged(RestContext sender, ServerConnectionChangedEventArgs args)
        {
            IsEnabled = args.ConnectionEstablished;
            if (IsEnabled) Initialize();
            else Terminate();
        }

        private void Initialize()
        {
            RefreshRulesAsync();
            InitializeEventHandlers();
        }

        private void Terminate()
        {
            TerminateEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            var eventService = _restContext.Client.EventService;
            eventService.RuleAdded += EventServiceOnRuleAdded;
            eventService.RuleRemoved += EventServiceOnRuleRemoved;

            Debug.WriteLine($"Initializing Event Handlers for {nameof(RulesViewModel)}");
        }

        private void TerminateEventHandlers()
        {
            var eventService = _restContext.Client.EventService;
            eventService.RuleAdded -= EventServiceOnRuleAdded;
            eventService.RuleRemoved -= EventServiceOnRuleRemoved;
            Debug.WriteLine($"Terminating Event Handlers for {nameof(RulesViewModel)}");
        }

        private void EventServiceOnRuleAdded(object sender, RuleAddedEvent eventObject)
        {
            //if (Rules.IsNullOrEmpty()) return;

            var rule = eventObject.Rule.ToViewModel();
            DispatcherHelper.RunAsync(() =>
            {
                Rules.Add(rule);
                if (rule.FilterBy(_filterCsv))
                    FilteredRules.Add(rule);
            });
        }

        private void EventServiceOnRuleRemoved(object sender, RuleRemovedEvent eventObject)
        {
            if (Rules.IsNullOrEmpty()) return;

            var rule = _rules.FirstOrDefault(r => r.Uid == eventObject.Rule.Uid);
            if (rule == null) return;
            DispatcherHelper.RunAsync(() =>
            {
                Rules.Remove(rule);
                FilteredRules.Remove(rule);
            });
        }

        private async void RefreshRulesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            var rules = await Task.Run(() => _restContext.Client.RuleService.GetRules());
            Rules = rules?.ToViewModels();
            CurrentRule = _rules?.FirstOrDefault();

            IsBusy = false;
        }

        #endregion
    }
}
