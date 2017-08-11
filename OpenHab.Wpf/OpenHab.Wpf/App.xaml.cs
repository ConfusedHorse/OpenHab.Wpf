﻿using System;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Threading;
using OpenHab.Wpf.View;
using OpenHab.Wpf.View.Dialogue;

namespace OpenHab.Wpf
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private const bool AutoConnectToServer = false;

        private void OpenHabWpf_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            DispatcherHelper.Initialize();
            PrepareContext(mainWindow);
        }

        private static void PrepareContext(Window owner)
        {
            bool? canConnectToServer = false;
            while (canConnectToServer != null && !(bool)canConnectToServer)
            {
                var serverAddressDialogue = new ServerAddressDialogue(AutoConnectToServer) {Owner = owner};
                canConnectToServer = serverAddressDialogue.ShowDialog();
            }
        }

        private void OpenHabWpf_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
