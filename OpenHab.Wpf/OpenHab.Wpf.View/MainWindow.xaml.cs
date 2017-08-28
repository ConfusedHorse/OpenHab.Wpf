using System.Windows;
using GalaSoft.MvvmLight.Threading;
using OpenHab.Wpf.View.Dialogue;
using OpenHab.Wpf.View.Module;
using OpenHAB.NetRestApi.Models.Events;

namespace OpenHab.Wpf.View
{
    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
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

        internal static async void PrepareContextAsync(Window owner, bool blockAutoconnect = false)
        {
            var serverViewModel = ViewModelLocator.Instance.ServerViewModel;
            serverViewModel.RestContext.ConnectionTerminated += RestContextOnConnectionTerminated;

            while (true)
            {
                if (!blockAutoconnect && serverViewModel.ConnectAutomatically && serverViewModel.RestContext.Reconnect())
                {
                    serverViewModel.SaveIpAddress();
                    break;
                }
                var serverAddressDialogue = new ServerAddressDialogue {Owner = owner};
                await DispatcherHelper.RunAsync(() => serverAddressDialogue.ShowDialog());

                if (serverViewModel.ConnectionEstablished) break;
            }
        }

        private static void RestContextOnConnectionTerminated(object sender, TerminatedUnexpectedlyEvent eventObject)
        {
            ViewModelLocator.Instance.ServerViewModel.IpAddressIsValid = false;
            DispatcherHelper.RunAsync(() => PrepareContextAsync(Application.Current.MainWindow));
        }

        private void ConnectToServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            PrepareContextAsync(this, true);
        }
    }
}