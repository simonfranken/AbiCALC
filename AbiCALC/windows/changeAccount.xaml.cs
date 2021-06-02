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
    /// Interaction logic for changeAccount.xaml
    /// </summary>
    public partial class changeAccount : Window, IWindow
    {
        public changeAccount()
        {
            InitializeComponent();
            accountList.getColor = (IName) => { return new Color { A = 255, R = 255 }; };
            accountList.setCollection(new System.Collections.ObjectModel.ObservableCollection<IName>(serialization.database.singleton.profiles));
        }

        private void newAcc(object sender, MouseButtonEventArgs e)
        {
            ((App)App.Current).createNewAccount();
            close_clicked(null, null);
        }
        private void okC(object sender, MouseButtonEventArgs e)
        {
            IName x = accountList.getSelected();
            if(x != null) 
            {
                serialization.database.load(serialization.database.getInfo((data)x));
                close_clicked(null, null);
            }
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
    }
}
