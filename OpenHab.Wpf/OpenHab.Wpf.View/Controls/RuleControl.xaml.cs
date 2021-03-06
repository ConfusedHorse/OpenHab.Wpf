﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OpenHab.Wpf.View.Module;
using OpenHab.Wpf.ViewModel.ViewModels;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für RuleControl.xaml
    /// </summary>
    public partial class RuleControl : UserControl
    {
        public static readonly DependencyProperty EditableProperty =
            DependencyProperty.Register("Editable", typeof(bool), typeof(RuleControl),
                new PropertyMetadata(default(bool)));

        public RuleControl()
        {
            InitializeComponent();
        }

        public bool Editable
        {
            get => (bool)GetValue(EditableProperty);
            set => SetValue(EditableProperty, value);
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

        private void Dummy_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var rulesViewModel = ViewModelLocator.RulesViewModel;
            if (rulesViewModel.CurrentRule.IsRuleDummy)
                rulesViewModel.CreateNewRule();
        }
    }
}
