using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Framework.UI.Controls;
using GalaSoft.MvvmLight.Threading;
using OpenHab.Wpf.View.Dialogue;
using OpenHab.Wpf.View.Module;
using OpenHAB.NetRestApi.Models.Events;
using Window = System.Windows.Window;

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
            Closing += OnClosing;
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DispatcherHelper.Initialize();
            PrepareContextAsync(this);
        }

        internal static async void PrepareContextAsync(Window owner, bool blockAutoconnect = false)
        {
            var serverViewModel = ViewModelLocator.ServerViewModel;
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
            ViewModelLocator.ServerViewModel.IpAddressIsValid = false;
            DispatcherHelper.RunAsync(() => PrepareContextAsync(Application.Current.MainWindow));
        }

        private void ConnectToServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            PrepareContextAsync(this, true);
        }

        private static async void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            var rulesViewModel = ViewModelLocator.RulesViewModel;
            var currentRule = rulesViewModel.CurrentRule;
            if (currentRule == null || !currentRule.UnsavedChanges) return;
            cancelEventArgs.Cancel = true;

            var saveChanges = await MessageDialog.ShowAsync(Properties.Resources.SaveChanges,
                Properties.Resources.UnsavedChanges, MessageBoxButton.YesNoCancel);

            if (saveChanges == MessageBoxResult.Yes)
                await Task.Run(() => currentRule.SaveChangesAsync());
            if (saveChanges == MessageBoxResult.Cancel)
                return;

            currentRule.UnsavedChanges = false;
            Application.Current.MainWindow.Close();
        }

        private void ExpandOptions(object sender, RoutedEventArgs e)
        {
            SettingsFlyout.IsExpanded = true;
        }
    }
}