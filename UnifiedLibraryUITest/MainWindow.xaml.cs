using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Threading;
using UnifiedLibraryUITest.Accounts;

namespace UnifiedLibraryUITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        //<Grid x:Name="ActionAddAcountGrid" 
        //HorizontalAlignment="Center" 
        //Height="230" Margin="10" 
        //VerticalAlignment="Center" Width="440" 
        private static Dictionary<Account,Grid> accounts = new Dictionary<Account, Grid>();
        private static int currentSelection = 0;
        BackgroundWorker backgroundWorker = new BackgroundWorker();


        private void initGrids(){
            
            accounts.Add(new Account("", -1, "Add an Account"), ActionAddAcountGrid);
            Grid temp;
            accounts.Add(new Account("foo", 25, "Compte Test"), temp = new Grid());
            temp.Width = ActionAddAcountGrid.Width;
            temp.Height = ActionAddAcountGrid.Height;
            temp.HorizontalAlignment = ActionAddAcountGrid.HorizontalAlignment;
            temp.VerticalAlignment = ActionAddAcountGrid.VerticalAlignment;
            Thickness marg = new Thickness(ActionAddAcountGrid.Margin.Left, ActionAddAcountGrid.Margin.Top, (ActionAddAcountGrid.Margin.Right + 10 + temp.Width), ActionAddAcountGrid.Margin.Bottom);
            temp.Margin = marg;
            temp.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\account.png")));
            MainGrid.Children.Add(temp);
        }

        public MainWindow(){
            InitializeComponent();
           
            Header.MouseDown += Window_MouseDown;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            initGrids();
        }
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e){
            for (int i = 0; i < 100; ++i){
                backgroundWorker.ReportProgress(i);
                System.Threading.Thread.Sleep(50);
            }
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }
        private void MainActionGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e){
            
        }

        private void MainActionGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void MainActionGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MainActionGrid_MouseEnter(object sender, MouseEventArgs e){

            //var image = sender as Grid;
            //image.ImageSource = b;
            var grid = sender as Grid;
            grid.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\power-logo-hover.png")));


        }

        private void MainActionGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            grid.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\power-logo.png")));

        }

        private void prevAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\back-hover.png")));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void prevAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\back.png")));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void nextAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\next-hover.png")));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void nextAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\next.png")));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void ActionAddAcountGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\add-hover.png")));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void ActionAddAcountGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var img = sender as Grid;
            var brush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"C:\Users\Axel\Source\Workspaces\Workspace\UnifiedLibraryV1\UnifiedLibraryUITest\Images\add.png")));
            brush.Stretch = Stretch.Uniform;
            img.Background = brush;
        }

        private void ActionAddAcountGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e){
            CreateAccount caWin = new CreateAccount();
            caWin.ShowDialog();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

    }

}
