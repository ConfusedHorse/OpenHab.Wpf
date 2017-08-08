using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OpenHab.Wpf.View.Resources.Helpers
{
    public partial class CircularBusyIndicator
    {
        #region Data

        private readonly DispatcherTimer _animationTimer;
        private const byte Speed = 5;
        private double _offset = 50;

        #endregion

        #region Constructor

        public CircularBusyIndicator()
        {
            InitializeComponent();
            _animationTimer =
                new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher)
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 75)
                };
        }

        #region DependencyProperties

        public bool Rainbow
        {
            get => (bool) GetValue(RainbowProperty);
            set => SetValue(RainbowProperty, value);
        }

        private static readonly DependencyProperty RainbowProperty =
            DependencyProperty.Register("Rainbow", typeof(bool), typeof(CircularBusyIndicator),
                new PropertyMetadata(false));

        public Brush Fill
        {
            get => (Brush) GetValue(FillProperty);
            set
            {
                SetValue(FillProperty, value);
                SetNewFill();
            }
        }

        private static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(CircularBusyIndicator),
                new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        public double Size
        {
            get => (double) GetValue(SizeProperty);
            set
            {
                SetValue(SizeProperty, value);
                ResetPositions();
            }
        }

        private static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(CircularBusyIndicator),
                new PropertyMetadata(32.0));

        #endregion

        #endregion

        #region Private Methods

        private void Start()
        {
            _animationTimer.Tick += HandleAnimationTick;
            _animationTimer.Start();
        }

        private void Stop()
        {
            _animationTimer.Stop();
            _animationTimer.Tick -= HandleAnimationTick;
        }

        private void HandleAnimationTick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
            if (!Rainbow) return;
            PickNewFill();
            SetNewFill();
        }

        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            ResetPositions();

            if (!Rainbow) return;
            Fill = new SolidColorBrush(Color.FromArgb(187, 255, 0, 0));
        }

        private void ResetPositions()
        {
            SetSize();
            SetNewFill();

            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(C0, offset, 0.0, step);
            SetPosition(C1, offset, 1.0, step);
            SetPosition(C2, offset, 2.0, step);
            SetPosition(C3, offset, 3.0, step);
            SetPosition(C4, offset, 4.0, step);
            SetPosition(C5, offset, 5.0, step);
            SetPosition(C6, offset, 6.0, step);
            SetPosition(C7, offset, 7.0, step);
            SetPosition(C8, offset, 8.0, step);
        }

        private void PickNewFill()
        {
            var color = ((SolidColorBrush) Fill).Color;
            if (color.R == 255 && color.G < 255 && color.B == 0) color.G += Speed;
            if (color.G == 255 && color.R > 0 && color.B == 0) color.R -= Speed;
            if (color.G == 255 && color.B < 255 && color.R == 0) color.B += Speed;
            if (color.B == 255 && color.G > 0 && color.R == 0) color.G -= Speed;
            if (color.B == 255 && color.R < 255 && color.G == 0) color.R += Speed;
            if (color.R == 255 && color.B > 0 && color.G == 0) color.B -= Speed;
            Fill = new SolidColorBrush(color);
        }

        private void SetNewFill()
        {
            C0.Fill = Fill;
            C1.Fill = Fill;
            C2.Fill = Fill;
            C3.Fill = Fill;
            C4.Fill = Fill;
            C5.Fill = Fill;
            C6.Fill = Fill;
            C7.Fill = Fill;
            C8.Fill = Fill;
        }

        private void SetSize()
        {
            Width = Size;
            Height = Size;
            Canvas.Width = Size;
            Canvas.Height = Size;

            var itemSize = Size / 6;

            // ReSharper disable PossibleLossOfFraction
            C0.Width = itemSize;
            C1.Width = itemSize;
            C2.Width = itemSize;
            C3.Width = itemSize;
            C4.Width = itemSize;
            C5.Width = itemSize;
            C6.Width = itemSize;
            C7.Width = itemSize;
            C8.Width = itemSize;

            C0.Height = itemSize;
            C1.Height = itemSize;
            C2.Height = itemSize;
            C3.Height = itemSize;
            C4.Height = itemSize;
            C5.Height = itemSize;
            C6.Height = itemSize;
            C7.Height = itemSize;
            C8.Height = itemSize;
            // ReSharper restore PossibleLossOfFraction

            _offset = Size / 2.4;
        }

        private void SetPosition(Ellipse ellipse, double offset,
            double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, _offset
                                                  + Math.Sin(offset + posOffSet * step) * _offset);

            ellipse.SetValue(Canvas.TopProperty, _offset
                                                 + Math.Cos(offset + posOffSet * step) * _offset);
        }

        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void HandleVisibleChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var isVisible = (bool) e.NewValue;

            if (isVisible)
                Start();
            else
                Stop();
        }

        #endregion
    }
}