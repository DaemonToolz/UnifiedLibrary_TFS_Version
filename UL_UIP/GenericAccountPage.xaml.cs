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
    public partial class GenericAccountPage : Page
    {
        public bool IsPrincipal { get; private set; }

        public GenericAccountPage()
        {
            InitializeComponent();
        }

        public void SetPrincipal(bool newState)
        {
            IsPrincipal = newState;
            if (IsPrincipal)
                AccountType.Content = "Main Account";
            else
                AccountType.Content = "Secondary Accounty";
        }
    }
}
