using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Elysium;
using Framework.UI;
using Framework.UI.Controls;
using OpenHab.Wpf.View.Module;

namespace OpenHab.Wpf.View.Controls
{
    /// <summary>
    /// Interaktionslogik für SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        private bool _languageLoaded;

        public SettingsControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            InitializeThemeOptions();
            InitializeLanguageOptions();
        }

        private void InitializeThemeOptions()
        {
            ThemeToggle.IsChecked = ViewModelLocator.Settings.Theme == Theme.Dark.ToString();
        }

        private void SetDarkTheme(object sender, RoutedEventArgs e)
        {
            var openHabWpf = Application.Current as ElysiumApplication;
            if (openHabWpf == null) return;

            openHabWpf.Theme = Theme.Dark;
            ViewModelLocator.Settings.Theme = Theme.Dark.ToString();
        }

        private void SetLightTheme(object sender, RoutedEventArgs e)
        {
            var openHabWpf = Application.Current as ElysiumApplication;
            if (openHabWpf == null) return;

            openHabWpf.Theme = Theme.Light;
            ViewModelLocator.Settings.Theme = Theme.Light.ToString();
        }

        private void InitializeLanguageOptions()
        {
            LanguageComboBox.Items.Add(Properties.Resources.English);
            LanguageComboBox.Items.Add(Properties.Resources.German);

            var currentLocale = Thread.CurrentThread.CurrentCulture.Name;

            var optionLabel = Properties.Resources.English;
            if (currentLocale == "de-DE")
                optionLabel = Properties.Resources.German;
            // add other languages here

            var selection = LanguageComboBox.Items.OfType<string>()
                .FirstOrDefault(tb => tb == optionLabel);
            if (selection != null)
                LanguageComboBox.SelectedItem = selection;

            _languageLoaded = true;
        }

        private async void LanguageComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_languageLoaded) return;
            var selection = e.AddedItems[0].ToString();

            var reloadMainwindow = await MessageDialog.ShowAsync(Properties.Resources.ReloadWithNewCulture,
                Properties.Resources.MustReloadForNewCulture, MessageBoxButton.OKCancel, owner: Application.Current.MainWindow);

            if (reloadMainwindow == MessageBoxResult.Cancel)
            { //restore
                _languageLoaded = false;
                var oldSelection = e.RemovedItems[0].ToString();
                LanguageComboBox.SelectedItem = oldSelection;
                _languageLoaded = true;
            }
            else
            {
                if (selection == Properties.Resources.German)
                    ReloadWithNewCulture("de-DE");
                if (selection == Properties.Resources.English)
                    ReloadWithNewCulture("en-GB");
                // add other languages here
            }
        }

        private static void ReloadWithNewCulture(string locale)
        {
            var culture = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var oldMainWindow = Application.Current.MainWindow;
            var newMainWindow = new MainWindow { Language = XmlLanguage.GetLanguage(culture.IetfLanguageTag) };
            Application.Current.MainWindow = newMainWindow;

            newMainWindow.Show();
            oldMainWindow.Close();
    }
    }
}
