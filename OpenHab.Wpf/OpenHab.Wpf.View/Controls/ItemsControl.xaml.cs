using System.Windows;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für ItemsControl.xaml
    /// </summary>
    public partial class ItemsControl : UserControl
    {
        public static readonly DependencyProperty AllowRuleOperationsProperty = DependencyProperty.Register("AllowDragOperations", typeof(bool), typeof(ItemsControl), new PropertyMetadata(default(bool)));

        public ItemsControl()
        {
            InitializeComponent();
        }

        public bool AllowRuleOperations
        {
            get => (bool) GetValue(AllowRuleOperationsProperty);
            set => SetValue(AllowRuleOperationsProperty, value);
        }
    }
}
