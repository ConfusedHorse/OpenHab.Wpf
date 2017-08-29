using System.Windows;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für TriggersControl.xaml
    /// </summary>
    public partial class TriggersControl : UserControl
    {
        public static readonly DependencyProperty AllowDragOperationsProperty =
            DependencyProperty.Register("AllowDragOperations", typeof(bool), typeof(TriggersControl),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty AllowDeleteOperationsProperty =
            DependencyProperty.Register("AllowDeleteOperations", typeof(bool), typeof(TriggersControl),
                new PropertyMetadata(default(bool)));

        public TriggersControl()
        {
            InitializeComponent();
        }

        public bool AllowDragOperations
        {
            get => (bool)GetValue(AllowDragOperationsProperty);
            set => SetValue(AllowDragOperationsProperty, value);
        }

        public bool AllowDeleteOperations
        {
            get => (bool)GetValue(AllowDeleteOperationsProperty);
            set => SetValue(AllowDeleteOperationsProperty, value);
        }
    }
}
