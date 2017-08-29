using System.Windows;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    ///     Interaktionslogik für ThingControl.xaml
    /// </summary>
    public partial class ThingControl : UserControl
    {
        public static readonly DependencyProperty AllowDragOperationsProperty =
            DependencyProperty.Register("AllowDragOperations", typeof(bool), typeof(ThingControl),
                new PropertyMetadata(default(bool)));

        public ThingControl()
        {
            InitializeComponent();
        }

        public bool AllowDragOperations
        {
            get => (bool) GetValue(AllowDragOperationsProperty);
            set => SetValue(AllowDragOperationsProperty, value);
        }
    }
}