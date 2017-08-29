using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für ThingsControl.xaml
    /// </summary>
    public partial class ThingsControl : UserControl
    {
        public static readonly DependencyProperty AllowDragOperationsProperty =
            DependencyProperty.Register("AllowDragOperations", typeof(bool), typeof(ThingsControl),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty OrientationProperty = 
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ThingsControl), 
                new PropertyMetadata(default(Orientation)));
        
        public ThingsControl()
        {
            InitializeComponent();
            ((INotifyCollectionChanged) Things.Items).CollectionChanged += ThingsChanged;
        }

        public bool AllowDragOperations
        {
            get => (bool)GetValue(AllowDragOperationsProperty);
            set => SetValue(AllowDragOperationsProperty, value);
        }
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        private void ThingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ThingsBehavior.AnimateIn();
        }
    }
}
