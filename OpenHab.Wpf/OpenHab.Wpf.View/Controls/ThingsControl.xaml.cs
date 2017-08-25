using System.Collections.Specialized;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für ThingsControl.xaml
    /// </summary>
    public partial class ThingsControl : UserControl
    {
        public ThingsControl()
        {
            InitializeComponent();
            ((INotifyCollectionChanged) Things.Items).CollectionChanged += ThingsChanged;
        }

        private void ThingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Behavior.AnimateIn();
        }
    }
}
