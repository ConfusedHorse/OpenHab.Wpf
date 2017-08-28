using System;
using System.Diagnostics;
using System.Threading.Tasks;
using OpenHab.Wpf.CrossCutting.Events;
using OpenHAB.NetRestApi.Models.Events;
using OpenHAB.NetRestApi.RestApi;
using OpenHAB.NetRestApi.Services;

namespace OpenHab.Wpf.CrossCutting.Context
{
    public class RestContext
    {
        #region Fields

        private readonly GlobalContext _globalContext;
        private bool _online;

        #endregion

        public OpenHabRestClient Client { get; private set; }

        public RestContext(GlobalContext globalContext)
        {
            _globalContext = globalContext;
        }

        #region Properties

        public string ServerAddress
        {
            get => _globalContext.Settings.ServerAddress;
            set
            {
                _globalContext.Settings.ServerAddress = value;
                if (Reconnect(value)) _globalContext.Settings.Save();
            }
        }

        public bool Online
        {
            get => _online;
            set
            {
                _online = value;
                Debug.WriteLine(_online ? "Connection has been established!" : "Connection has been terminated!");
                ConnectionChanged?.Invoke(this, new ServerConnectionChangedEventArgs(_online, ServerAddress));

                if (_online) Task.Run(() => Client.EventService.InitializeAsync());
                else Client?.EventService.TerminateAsync();
            }
        }

        #endregion

        #region Events

        public event ServerConnectionChangedEventHandler ConnectionChanged;
        public event AttemptedReconnectEventHandler AttemptedReconnect;
        public event TerminatedUnexpectedlyEventHandler ConnectionTerminated;

        #endregion

        #region Public Methods

        public bool Reconnect(string checkAddress = null)
        {
            try
            {
                Client = OpenHAB.NetRestApi.RestApi.OpenHab.CreateRestClient(checkAddress ?? ServerAddress);
                var eventService = Client.EventService;
                eventService.TerminatedUnexpectedly += HandleUnexpectedTermination;
                eventService.AttemptedReconnect += OnAttemptedReconnect;
                return true;
            }
            catch (ArgumentException)
            {
                if (Client?.EventService == null) return false;
                var eventService = Client.EventService;
                eventService.TerminatedUnexpectedly -= HandleUnexpectedTermination;
                eventService.AttemptedReconnect -= OnAttemptedReconnect;
                return false;
            }
        }

        #endregion

        #region Private Methods

        private void HandleUnexpectedTermination(object sender, TerminatedUnexpectedlyEvent eventobject)
        {
            Online = false;
            ConnectionTerminated?.Invoke(sender, eventobject);
        }

        private void OnAttemptedReconnect(EventService sender, AttemptedReconnectEvent eventobject)
        {
            AttemptedReconnect?.Invoke(sender, eventobject);
        }

        #endregion
    }
}