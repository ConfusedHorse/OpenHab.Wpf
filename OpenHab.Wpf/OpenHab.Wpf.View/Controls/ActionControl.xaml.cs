using System.Windows;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    ///     Interaktionslogik für ActionControl.xaml
    /// </summary>
    public partial class ActionControl : UserControl
    {
        public static readonly DependencyProperty AllowDragOperationsProperty =
            DependencyProperty.Register("AllowDragOperations", typeof(bool), typeof(ActionControl),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty AllowDeleteOperationsProperty =
            DependencyProperty.Register("AllowDeleteOperations", typeof(bool), typeof(ActionControl),
                new PropertyMetadata(default(bool)));

        public ActionControl()
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