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
    /// Logique d'interaction pour NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {

        List<NotificationItem> Notifications = new List<NotificationItem>();
        public NotificationsPage()
        {
            InitializeComponent();
            for (int i = 0; i < 10; ++i) {
                var notif = new NotificationItem();
                notif.NotificationTitleStr = "N"+i;
                notif.NotificationContentStr = "D" + i;

                Notifications.Add(notif);
                    
            }

            NotificationsList.ItemsSource = Notifications;
        }



        private void Delete_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\delete-hover.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void Delete_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(@"Images\delete.png", UriKind.Relative)));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void Delete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (NotificationsList.SelectedItem == null) return;
            var selected = NotificationsList.SelectedIndex;
            Notifications.RemoveAt(selected);
            NotificationsList.ItemsSource = null;
            NotificationsList.ItemsSource = Notifications;
            NotificationsList.SelectedItem = null;
        }

        private void NotificationsList_SelectionChanged(object sender, SelectionChangedEventArgs e){
            if (NotificationsList.SelectedItem == null) return;
            var selected = NotificationsList.SelectedItem as NotificationItem;
            selected.NotificationRead = true;
            
        }
    }
}
