using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenHab.Wpf.View.Controls.Custom
{
    /// <summary>
    ///     Interaktionslogik für TimePicker.xaml
    /// </summary>
    public partial class TimePicker
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(TimeSpan), typeof(TimePicker),
                new UIPropertyMetadata(DateTime.Now.TimeOfDay, OnValueChanged));

        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof(int), typeof(TimePicker),
                new UIPropertyMetadata(0, OnTimeChanged));

        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(int), typeof(TimePicker),
                new UIPropertyMetadata(0, OnTimeChanged));

        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(TimePicker),
                new UIPropertyMetadata(0, OnTimeChanged));

        public static readonly DependencyProperty IncrementProperty = 
            DependencyProperty.Register("Increment", typeof(int), typeof(TimePicker), 
                new UIPropertyMetadata(5));

        public TimePicker()
        {
            InitializeComponent();
        }

        public TimeSpan Value
        {
            get => (TimeSpan) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public int Increment
        {
            get => (int) GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }

        public int Hours
        {
            get => (int) GetValue(HoursProperty);
            set => SetValue(HoursProperty, Validate(value, 23));
        }

        public int Minutes
        {
            get => (int) GetValue(MinutesProperty);
            set => SetValue(MinutesProperty, Validate(value, 59));
        }

        public int Seconds
        {
            get => (int) GetValue(SecondsProperty);
            set => SetValue(SecondsProperty, Validate(value, 59));
        }

        private static int Validate(int value, int upper, int lower = 0)
        {
            if (value > upper) return upper;
            if (value < lower) return lower;
            return value;
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as TimePicker;
            if (control == null) return;
            control.Hours = ((TimeSpan) e.NewValue).Hours;
            control.Minutes = ((TimeSpan) e.NewValue).Minutes;
            control.Seconds = ((TimeSpan) e.NewValue).Seconds;
        }

        private static void OnTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as TimePicker;
            if (control == null) return;
            control.Value = new TimeSpan(control.Hours, control.Minutes, control.Seconds);
        }

        private void Hours_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            IncreaseHours(e.Delta > 0);
        }

        private void Minutes_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            IncreaseMinutes(e.Delta > 0);
        }

        private void Seconds_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            IncreaseSeconds(e.Delta > 0);
        }

        private void IncreaseHours(bool increase = true)
        {
            if (increase)
                if (Hours < 23) Hours++;
                else Hours = 0;
            else
                if (Hours > 0) Hours--;
                else Hours = 23;
        }

        private void IncreaseMinutes(bool increase = true)
        {
            if (increase)
                if (Minutes + Increment <= 59) Minutes += Increment;
                else
                {
                    Minutes = 0;
                    IncreaseHours();
                }
            else
                if (Minutes - Increment >= 0) Minutes -= Increment;
                else
                {
                    Minutes = 60 - Increment;
                    IncreaseHours(false);
                }
        }

        private void IncreaseSeconds(bool increase = true)
        {
            if (increase)
                if (Seconds + Increment <= 59) Seconds += Increment;
                else
                {
                    Seconds = 0;
                    IncreaseMinutes();
                }
            else
                if (Seconds - Increment >= 0) Seconds -= Increment;
                else
                {
                    Seconds = 60 - Increment;
                    IncreaseMinutes(false);
                }
        }

        private void Hours_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(HoursTextBox)) Hours = 0;
        }

        private void Minutes_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(MinutesTextBox)) Minutes = 0;
        }

        private void Seconds_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(SecondsTextBox)) Seconds = 0;
        }
    }
}