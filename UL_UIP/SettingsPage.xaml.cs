using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UL_UIP
{
    /// <summary>
    /// Logique d'interaction pour GenericAccountPage.xaml
    /// </summary>
    public partial class SettingsPage: Page
    {
        public bool IsPrincipal { get; private set; }
        
        public Dictionary<String, List<String>> AvailableThemes = new Dictionary<string, List<string>>();


        public SettingsPage()
        {
            var colors = new List<String>();
            colors.Add("#000000"); // Background
            colors.Add("#FFFFFF"); // Writing
            AvailableThemes.Add("Dark", colors);

            colors = new List<string>();
            colors.Add("#FFFFFF"); // Background
            colors.Add("#000000"); // Writing


            AvailableThemes.Add("Original", colors);
            
            InitializeComponent();

            ThemeSelector.ItemsSource = AvailableThemes.Keys;
            //ThemeSelector.SelectedItem = ThemeSelector.Items[0];

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeSelector.SelectedItem == null) return;
            var selected = ThemeSelector.SelectedItem as String;
            

            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(AvailableThemes[selected][0]));
            MainWindow.MyNotifications.MainGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(AvailableThemes[selected][0]));
            MainWindow.MainUIColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(AvailableThemes[selected][0]));
            MainWindow.UpdateUiColor();
            foreach (var visual in MainGrid.Children){

                if (visual.GetType().Equals(typeof(Label)))
                    ((Label)visual).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(AvailableThemes[selected][1]));
            }

            foreach (var visual in MainWindow.MyNotifications.MainGrid.Children)
            {
                if (visual.GetType().Equals(typeof(Label)))
                    ((Label)visual).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(AvailableThemes[selected][1]));
            }

            foreach (var visual in MainWindow.MyAccounts)
            {
                visual.MainGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(AvailableThemes[selected][0]));

                foreach (var grid in visual.MainGrid.Children)
                    if (grid.GetType().Equals(typeof(Label)))
                        ((Label)grid).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(AvailableThemes[selected][1]));
            }
        }

        
    }
}
