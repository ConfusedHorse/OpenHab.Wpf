using System;
using System.Diagnostics;
using OpenHab.Wpf.CrossCutting.Events;
using OpenHAB.NetRestApi.RestApi;

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
                ConnectionChanged?.Invoke(this, new ServerConnectionChangedEventArgs(true, ServerAddress));
            }
        }

        #endregion

        #region Events

        public event ServerConnectionChangedEventHandler ConnectionChanged;

        #endregion

        #region Public Methods

        public bool Reconnect(string checkAddress = null)
        {
            try
            {
                Client = OpenHAB.NetRestApi.RestApi.OpenHab.CreateRestClient(checkAddress ?? ServerAddress);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion
    }
}