using System.Windows;
using System.Windows.Controls;
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
