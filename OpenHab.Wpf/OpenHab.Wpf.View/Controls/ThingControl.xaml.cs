using System.Windows;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für ThingControl.xaml
    /// </summary>
    public partial class ThingControl : UserControl
    {
        public static readonly DependencyProperty AllowRuleOperationsProperty = DependencyProperty.Register("AllowRuleOperations", typeof(bool), typeof(ThingControl), new PropertyMetadata(default(bool)));

        public ThingControl()
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
