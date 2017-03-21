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
    /// Logique d'interaction pour NotificationItem.xaml
    /// </summary>
    public partial class NotificationItem : UserControl
    {
        public NotificationItem()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty NotificationReadProperty = DependencyProperty.Register("NotificationRead", typeof(Boolean), typeof(NotificationItem), new PropertyMetadata(false));

        public Boolean NotificationRead
        {
            get { return Boolean.Parse(GetValue(NotificationReadProperty).ToString()); }
            set { SetValue(NotificationReadProperty, value); }
        }


        public static readonly DependencyProperty NotificationTitleProperty =  DependencyProperty.Register("NotificationTitleStr", typeof(String), typeof(NotificationItem), new PropertyMetadata(""));

        public String NotificationTitleStr {
            get { return GetValue(NotificationTitleProperty).ToString(); }
            set { SetValue(NotificationTitleProperty, value); }
        }


        public static readonly DependencyProperty NotificationContentProperty = DependencyProperty.Register("NotificationContentStr", typeof(String), typeof(NotificationItem), new PropertyMetadata(""));

        public String NotificationContentStr
        {
            get { return GetValue(NotificationContentProperty).ToString(); }
            set { SetValue(NotificationContentProperty, value); }
        }
    }
}
