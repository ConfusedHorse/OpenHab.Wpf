﻿using System.Windows;
using OpenHab.Wpf.View.Module;

namespace OpenHab.Wpf.View.Dialogue
{
    /// <summary>
    /// Interaktionslogik für ServerAddressDialogue.xaml
    /// </summary>
    public partial class ServerAddressDialogue
    {
        public ServerAddressDialogue()
        {
            InitializeComponent();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Instance.ServerViewModel.SaveIpAddress();
            DialogResult = true;
        }

        private void CloseApplicationButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Application.Current.Shutdown();
        }

        private void ServerAddressDialogue_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Instance.ServerViewModel.InvalidateConnection();
        }
    }
}
