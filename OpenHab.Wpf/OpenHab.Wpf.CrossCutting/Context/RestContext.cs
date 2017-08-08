using System;
using OpenHAB.NetRestApi.RestApi;

namespace OpenHab.Wpf.CrossCutting.Context
{
    public class RestContext
    {
        private readonly GlobalContext _globalContext;

        public OpenHabRestClient Client { get; private set; }

        public RestContext(GlobalContext globalContext)
        {
            _globalContext = globalContext;
        }

        public string ServerAddress
        {
            get => _globalContext.Settings.ServerAddress;
            set
            {
                _globalContext.Settings.ServerAddress = value;
                if (Reconnect(value)) _globalContext.Settings.Save();
            }
        }

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
    }
}