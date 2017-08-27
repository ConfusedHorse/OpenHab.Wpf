using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OpenHab.Wpf.ViewModel.ViewModels;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für RuleControl.xaml
    /// </summary>
    public partial class RuleControl : UserControl
    {
        public RuleControl()
        {
            InitializeComponent();
        }

        private void RuleToggleSwitch_OnChecked(object sender, RoutedEventArgs e)
        {
            ToggleRule(true);
        }

        private void RuleToggleSwitch_OnUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleRule(false);
        }

        private void ToggleRule(bool enabled)
        {
            var rule = DataContext as RuleViewModel;
            rule?.ToggleRule(enabled);
        }
    }
}
