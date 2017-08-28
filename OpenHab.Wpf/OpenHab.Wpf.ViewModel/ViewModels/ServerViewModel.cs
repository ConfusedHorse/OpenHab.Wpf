using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Helpers;
using OpenHAB.NetRestApi.Models.Events;
using OpenHAB.NetRestApi.Services;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ServerViewModel : ViewModelBase
    {
        #region Fields

        private string _ipAddress;
        private bool _connectionEstablished;
        private bool? _ipAddressIsValid;
        private bool _checkingConnection;
        private DateTime _latestCheck;
        private readonly GlobalContext _globalContext;
        private bool _attemptingToReconnect;
        private int _reconnectionAttempts;

        #endregion

        public ServerViewModel(RestContext restContext, GlobalContext globalContext)
        {
            RestContext = restContext;
            _globalContext = globalContext;
            IpAddress = RestContext.ServerAddress;
        }

        #region Properties

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                IpAddressIsValid = string.IsNullOrEmpty(value) ? null : (bool?)IpHelper.ValidateIPv4(value);
                RaisePropertyChanged();
            }
        }

        public bool? IpAddressIsValid
        {
            get => _ipAddressIsValid;
            set
            {
                _ipAddressIsValid = value;
                if (value.HasValue && value.Value) CheckConnectionAsync();
                else ConnectionEstablished = false;
                RaisePropertyChanged();
            }
        }

        public bool ConnectionEstablished
        {
            get => _connectionEstablished;
            private set
            {
                _connectionEstablished = value;
                RaisePropertyChanged();
            }
        }

        public bool CheckingConnection
        {
            get => _checkingConnection;
            set
            {
                _checkingConnection = value;
                RaisePropertyChanged();
            }
        }

        public bool ConnectAutomatically
        {
            get => _globalContext.Settings.ConnectAutomatically;
            set
            {
                _globalContext.Settings.ConnectAutomatically = value;
                RaisePropertyChanged();
            }
        }

        public bool AttemptingToReconnect
        {
            get => _attemptingToReconnect;
            set
            {
                _attemptingToReconnect = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => Idle);
            }
        }

        public bool Idle => !AttemptingToReconnect;

        public int ReconnectionAttempts
        {
            get => _reconnectionAttempts;
            set
            {
                _reconnectionAttempts = value;
                RaisePropertyChanged();
            }
        }

        public RestContext RestContext { get; }

        #endregion

        #region Public Methods

        public void InvalidateConnection()
        {
            RestContext.Online = false;
        }

        public void SaveIpAddress()
        {
            RestContext.ServerAddress = _ipAddress;
            RestContext.Online = true;
        }

        public async void CheckConnectionAsync()
        {
            CheckingConnection = true;
            ConnectionEstablished = false;
            var start = DateTime.Now;
            _latestCheck = start;
            var connectionEstablished = false;

            if (IpAddressIsValid.HasValue && IpAddressIsValid.Value)
                connectionEstablished = await Task.Run(() => RestContext.Reconnect(IpAddress));

            if (start >= _latestCheck)
            {
                ConnectionEstablished = connectionEstablished;
                RestContext.AttemptedReconnect += EventServiceOnAttemptedReconnect;
                AttemptingToReconnect = false;
                ReconnectionAttempts = 0;
            }
            CheckingConnection = false;
        }

        private void EventServiceOnAttemptedReconnect(EventService sender, AttemptedReconnectEvent eventObject)
        {
            if (eventObject.Count > 0)
            {
                AttemptingToReconnect = true;
                ReconnectionAttempts = eventObject.Count;
            }
            else
            {
                AttemptingToReconnect = false;
                ReconnectionAttempts = 0;
            }
        }

        #endregion
    }
}