using System;
using OpenHab.Wpf.CrossCutting.Context;

namespace OpenHab.Wpf.CrossCutting.Events
{
    public delegate void ServerConnectionChangedEventHandler(RestContext sender, ServerConnectionChangedEventArgs eventArgsObject);

    public class ServerConnectionChangedEventArgs : EventArgs
    {
        public ServerConnectionChangedEventArgs(bool connectionEstablished, string serverAddress)
        {
            ConnectionEstablished = connectionEstablished;
            ServerAddress = serverAddress;
            ConnectionChangedTime = DateTime.Now;
        }

        public bool ConnectionEstablished { get; }

        public string ServerAddress { get; }

        public DateTime ConnectionChangedTime { get; }
    }
}