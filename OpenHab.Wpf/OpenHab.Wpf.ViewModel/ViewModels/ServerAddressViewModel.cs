using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Helper;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class ServerAddressViewModel : ViewModelBase
    {
        private string _ipAddress;
        private bool _connectionEstablished;
        private bool? _ipAddressIsValid;
        private bool _checkingConnection;

        public ServerAddressViewModel(RestContext restContext)
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
                RaisePropertyChanged(() => IpAddressIsValid);
            }
        }

        public bool? IpAddressIsValid
        {
            get => _ipAddressIsValid;
            private set
            {
                _ipAddressIsValid = value;
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

        public void SaveIpAddress()
        {
            RestContext.ServerAddress = _ipAddress;
        }

        public async void CheckConnectionAsync()
        {
            CheckingConnection = true;
            ConnectionEstablished = false;
            if (IpAddressIsValid.HasValue && IpAddressIsValid.Value)
                ConnectionEstablished = await Task.Run(() => RestContext.Reconnect(IpAddress));
            else
                ConnectionEstablished = false;
            CheckingConnection = false;
        }
    }
}