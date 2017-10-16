using System.Windows;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    ///     Interaktionslogik für ConditionControl.xaml
    /// </summary>
    public partial class ConditionControl : UserControl
    {
        public static readonly DependencyProperty AllowDragOperationsProperty =
            DependencyProperty.Register("AllowDragOperations", typeof(bool), typeof(ConditionControl),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty AllowDeleteOperationsProperty =
            DependencyProperty.Register("AllowDeleteOperations", typeof(bool), typeof(ConditionControl),
                new PropertyMetadata(default(bool)));

        public ConditionControl()
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