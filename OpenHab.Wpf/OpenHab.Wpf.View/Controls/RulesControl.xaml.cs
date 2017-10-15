using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Framework.UI.Controls;
using OpenHab.Wpf.View.Module;
using OpenHab.Wpf.ViewModel.Helper;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für RulesControl.xaml
    /// </summary>
    public partial class RulesControl : UserControl
    {
        public RulesControl()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)Rules.Items).CollectionChanged += RulesChanged;
        }

        private void RulesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RulesBehavior.AnimateIn();
        }

        private async void Rules_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var rulesViewModel = ViewModelLocator.Instance.RulesViewModel;
            var currentRule = rulesViewModel.CurrentRule;
            if (currentRule == null || !currentRule.UnsavedChanges) return;
            if (currentRule.Triggers.IsNullOrEmpty() 
                && currentRule.Conditions.IsNullOrEmpty() 
                && currentRule.Actions.IsNullOrEmpty()) return;

            var itemUnderMouse = GetListBoxItemUnderMouse(Rules);
            if (itemUnderMouse == null || itemUnderMouse.DataContext == currentRule) return;

            e.Handled = true;
            var saveChanges = await MessageDialog.ShowAsync(Properties.Resources.SaveChanges,
                Properties.Resources.UnsavedChanges, MessageBoxButton.YesNo, owner: Application.Current.MainWindow);

            if (saveChanges == MessageBoxResult.Yes) currentRule.SaveChangesAsync();
            else currentRule.UnsavedChanges = false;

            itemUnderMouse.IsSelected = true;
        }

        private static ListBoxItem GetListBoxItemUnderMouse(System.Windows.Controls.ItemsControl lb)
        {
            return (from object o in lb.Items select lb.ItemContainerGenerator.ContainerFromItem(o) as ListBoxItem)
                .FirstOrDefault(lbi => lbi != null && lbi.IsMouseOver);
        }
    }
}
