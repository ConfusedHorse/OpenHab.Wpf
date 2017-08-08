using System.Windows;
using System.Windows.Threading;
using OpenHab.Wpf.View;

namespace OpenHab.Wpf
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void OpenHabWpf_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void OpenHabWpf_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
