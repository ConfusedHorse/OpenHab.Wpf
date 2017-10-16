using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace OpenHab.Wpf.View.Resources.Helpers.Extensions
{
    public class HorizontalScrollBehavior : Behavior<ScrollViewer>
    {
        public bool IsInverted { get; set; }
        
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnLoaded;
            AssociatedObject.PreviewMouseWheel += OnPreviewMouseWheel;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewMouseWheel -= OnPreviewMouseWheel;
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var newOffset = IsInverted
                ? AssociatedObject.HorizontalOffset + e.Delta
                : AssociatedObject.HorizontalOffset - e.Delta;

            AssociatedObject.ScrollToHorizontalOffset(newOffset);
        }
    }
}