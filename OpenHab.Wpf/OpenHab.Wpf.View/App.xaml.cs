using System.Linq;
using System.Windows.Threading;

namespace OpenHab.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App
    {
        private void OpenHabWpf_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            switch (e.Exception)
            {
                case System.AggregateException ae:
                    if (ae.InnerExceptions.FirstOrDefault(ie => ie.GetType().ToString() == "JsonReaderException") != null)
                    {
                        var mainWindow = (MainWindow) Current.MainWindow;
                        View.MainWindow.PrepareContextAsync(mainWindow);
                    }
                    break;
            }
        }
    }
}
