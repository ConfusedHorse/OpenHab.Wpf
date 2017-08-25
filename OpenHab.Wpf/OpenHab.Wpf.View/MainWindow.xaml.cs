using System.Windows;
using GalaSoft.MvvmLight.Threading;
using OpenHab.Wpf.View.Dialogue;
using OpenHab.Wpf.View.Module;

namespace OpenHab.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            Loaded += MainWindow_OnLoaded;
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DispatcherHelper.Initialize();
            PrepareContextAsync(this);
        }

        private static async void PrepareContextAsync(Window owner)
        {
            var serverViewModel = ViewModelLocator.Instance.ServerViewModel;
            while (true)
            {
                if (serverViewModel.ConnectAutomatically && serverViewModel.RestContext.Reconnect())
                {
                    serverViewModel.SaveIpAddress();
                    break;
                }
                var serverAddressDialogue = new ServerAddressDialogue { Owner = owner };
                await DispatcherHelper.RunAsync(() => serverAddressDialogue.ShowDialog());

                if (serverViewModel.ConnectionEstablished) break;
            }
        }
    }
}
