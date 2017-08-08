using System.Windows;
using System.Windows.Controls;
using Ninject;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Module;
using OpenHab.Wpf.View.Module;

namespace OpenHab.Wpf.View.Dialogue
{
    /// <summary>
    /// Interaktionslogik für ServerAddressDialogue.xaml
    /// </summary>
    public partial class ServerAddressDialogue : Window
    {
        public ServerAddressDialogue()
        {
            InitializeComponent();
        }

        private void IpAddressTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModelLocator.Instance.ServerAddressViewModel.CheckConnectionAsync();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Instance.ServerAddressViewModel.SaveIpAddress();
            DialogResult = true;
        }

        private void CloseApplicationButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Application.Current.Shutdown();
        }
    }
}
