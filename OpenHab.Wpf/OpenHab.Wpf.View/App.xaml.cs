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
            switch (e.Exception.GetType().ToString())
            {
                case "NullReferenceException":
                    //e.Handled = true;
                    break;
            }
        }
    }
}
