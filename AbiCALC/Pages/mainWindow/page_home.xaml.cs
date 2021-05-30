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
using AbiCALC;

namespace AbiCALC.Pages.mainWindow
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class page_home : Page
    {
        public page_home()
        {
            InitializeComponent();
        }

        private void add_clicked(object sender, MouseButtonEventArgs e)
        {
            windows.MainWindow.singleton.set(typeof(page_add));
        }
    }
}
