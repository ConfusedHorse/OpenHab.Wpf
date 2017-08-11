using System;
using System.Linq;
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
            _delayDispatcherTimer.Tick += UpdateState; //catch multiple parallel events
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

        private void DimmerSlider_OnDragDelta(object sender, DragDeltaEventArgs e)
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

        #region Color

        private void ColorHueSlider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 0);
        }

        private void ColorHueSlider_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 0);
        }

        private void ColorHueSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 0);
        }

        private void ColorSaturationSlider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 1);
        }

        private void ColorSaturationSlider_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 1);
        }

        private void ColorSaturationSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 1);
        }

        private void ColorBrightnessSlider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 2);
        }

        private void ColorBrightnessSlider_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 2);
        }

        private void ColorBrightnessSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            ColorHueSliderValueChangedByUserInteraction(sender, 2);
        }

        private void ColorHueSliderValueChangedByUserInteraction(object sender, int index)
        {
            var source = (Slider)sender;
            if (!source.IsKeyboardFocused) return;

            var target = (ItemViewModel)DataContext;
            var values = target.State?.Split(',') ?? new[] { "0", "0", "0" };
            values[index] = Convert.ToInt32(source.Value).ToString();

            _latestStateCausedByUserInteraction = string.Join(",", values);
            _delayDispatcherTimer.Start();
        }

        #endregion

        #region Number
        
        private void NumberTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab) return;
            NumberTextBoxValueChangedByUserInteraction(sender);
        }

        private void NumberTextBoxValueChangedByUserInteraction(object sender)
        {
            var source = (TextBox)sender;
            if (!source.IsKeyboardFocused) return;

            var errors = Validation.GetErrors(source);
            if (errors.Any()) return;

            _latestStateCausedByUserInteraction = source.Text.Replace(',', '.');
            _delayDispatcherTimer.Start();
        }

        #endregion
    }
}