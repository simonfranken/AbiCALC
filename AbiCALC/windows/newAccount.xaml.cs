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
using System.Windows.Shapes;

namespace AbiCALC.windows
{
    /// <summary>
    /// Interaction logic for newAccount.xaml
    /// </summary>
    public partial class newAccount : Window, IWindow
    {
        public newAccount()
        {
            InitializeComponent();
            windowFrame.Content = new Pages.newAccount.Page1();
        }

        public void close_clicked(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        public void max_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        public void min_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ok_clicked(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
