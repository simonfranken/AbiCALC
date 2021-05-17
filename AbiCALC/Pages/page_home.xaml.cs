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

namespace AbiCALC.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class page_home : Page
    {
        Frame frame;

        public page_home(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void add_clicked(object sender, MouseButtonEventArgs e)
        {
            frame.Content = new page_add();
        }
    }
}
