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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int CurrentSelection = -1;
        public static List<GenericAccountPage> MyAccounts { get; private set; }
        public static SettingsPage MySettings { get; private set; }
        public static NotificationsPage MyNotifications { get; private set; }
        public static Brush MainUIColor { get; set; }
        public static Grid  MainGrid { get; private set; }
        // Template
        public static void UpdateUiColor()
        {
            MainGrid.Background = MainUIColor;
            //foreach (var visual in MainGrid)
        }

        //

        public MainWindow()
        {
            
            InitializeComponent();
            MainGrid = MainWindowGrid;
            MyAccounts = new List<GenericAccountPage>();
            MySettings = new SettingsPage();
            MyNotifications = new NotificationsPage();

            this.MainWindowGrid.MouseDown += Window_MouseDown;

            var account0 = new GenericAccountPage();
            account0.TitleLabel.Content = "Zygwyg";
            account0.SetPrincipal(true);
            MyAccounts.Add(account0);
            account0  = new GenericAccountPage();
            account0.TitleLabel.Content = "Viktor";
            account0.SetPrincipal(false);
            MyAccounts.Add(account0);
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MainActionGrid_MouseEnter(object sender, MouseEventArgs e)
        {

            //var image = sender as Grid;
            //image.ImageSource = b;
            var grid = sender as Grid;
            grid.Background = new ImageBrush(new BitmapImage(new Uri(@"Images\power-logo-hover.png", UriKind.Relative)));


        }

        private void MainActionGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            grid.Background = new ImageBrush(new BitmapImage(new Uri(@"Images\power-logo.png", UriKind.Relative)));

        }


        private void prevAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\back-hover.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void prevAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\back.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void nextAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\next-hover.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void nextAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\next.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }


        private void MainActionGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void NextActionGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentSelection--;
            if (CurrentSelection < 0)
                CurrentSelection = MyAccounts.Count - 1;
            AccountFrame.Navigate(MyAccounts[CurrentSelection]);
        }

        private void PrevActionGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentSelection++;
            if (CurrentSelection > MyAccounts.Count - 1)
                CurrentSelection = 0;

            AccountFrame.Navigate(MyAccounts[CurrentSelection]);
        }


        private void CreateAccountGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\add-hover.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void CreateAccountGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\add.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void CreateAccountGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var account0 = new GenericAccountPage();
            account0.TitleLabel.Content = "Account Number #" + (MyAccounts.Count+1);
            account0.SetPrincipal(false);
            MyAccounts.Add(account0);
            AccountFrame.Navigate(MyAccounts[MyAccounts.Count-1]);
        }

        private void SettingButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AccountFrame.Navigate(MySettings);
        }

        private void SettingButton_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\Settings-hover.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void SettingButton_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\Settings.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }


        private void NotificationGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\notification-hover.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void NotificationGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\notification.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void NotificationGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AccountFrame.Navigate(MyNotifications);
        }
    }
}

