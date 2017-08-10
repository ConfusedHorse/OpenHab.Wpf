using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using OpenHab.Wpf.ViewModel.ViewModels;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    ///     Interaktionslogik für ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        private DispatcherTimer _delayDispatcherTimer;
        private string _latestStateCausedByUserInteraction;

        public ItemControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _delayDispatcherTimer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(50)};
            _delayDispatcherTimer.Tick += UpdateState;
        }

        private void UpdateState(object sender, EventArgs eventArgs)
        {
            _delayDispatcherTimer.Stop();
            var target = (ItemViewModel)DataContext;
            target.SendCommandAsync(_latestStateCausedByUserInteraction);
        }

        #region Dimmer

        private void DimmerSlider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            DimmerSliderValueChangedByUserInteraction(sender);
        }

        private void DimmerSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            DimmerSliderValueChangedByUserInteraction(sender);
        }

        private void DimmerSliderValueChangedByUserInteraction(object sender)
        {
            var source = (Slider)sender;
            if (!source.IsKeyboardFocused) return;
            _latestStateCausedByUserInteraction = Convert.ToInt32(source.Value).ToString();
            _delayDispatcherTimer.Start();
        }

        #endregion
    }
}