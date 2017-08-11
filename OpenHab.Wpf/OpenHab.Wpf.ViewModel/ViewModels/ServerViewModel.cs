using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Helpers;

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

        #endregion

        public ServerViewModel(RestContext restContext)
        {
            RestContext = restContext;
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
            private set
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

            if (start >= _latestCheck) ConnectionEstablished = connectionEstablished;
            CheckingConnection = false;
        }

        #endregion
    }
}