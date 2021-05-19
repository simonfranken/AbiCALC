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
using System.Windows.Controls.Primitives;
using AbiCALC.customUI.ListSelection;
using AbiCALC.Pages;

namespace AbiCALC
{
    public partial class MainWindow : Window, IWindow
    {

        //Attributes
        List<Grid> windows = new List<Grid>();
        Dictionary<IName, Color> subjectColors = new Dictionary<IName, Color>();

        //Main-Method
        public MainWindow()
        {
            InitializeComponent();
            windowFrame.Content = new page_home(windowFrame);
        }

        //Events
        public void close_clicked(object sender, MouseButtonEventArgs e)
        {
            Close();
            ((App)App.Current).close();
        }
        public void max_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        public void min_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void home_clicked(object sender, MouseButtonEventArgs e)
        {
            windowFrame.Content = new page_home(windowFrame);
        }
        private void profile_clicked(object sender, MouseButtonEventArgs e)
        {
            windowFrame.Content = new page_profile();
        }
        private void add_clicked(object sender, MouseButtonEventArgs e)
        {
            windowFrame.Content = new page_add();
        }
    }
}
