using System.Collections.Specialized;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für RulesControl.xaml
    /// </summary>
    public partial class RulesControl : UserControl
    {
        public RulesControl()
        {
            InitializeComponent();
            ((INotifyCollectionChanged) Things.Items).CollectionChanged += ThingsChanged;
            ((INotifyCollectionChanged) Rules.Items).CollectionChanged += RulesChanged;
        }

        private void ThingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ThingsBehavior.AnimateIn();
        }

        private void RulesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RulesBehavior.AnimateIn();
        }
    }
}
