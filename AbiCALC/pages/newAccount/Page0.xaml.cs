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

namespace AbiCALC.Pages.newAccount
{
    /// <summary>
    /// Interaction logic for Page0.xaml
    /// </summary>
    public partial class Page0 : Page, IWizard
    {
        public static string name = "";
        public Page0()
        {
            InitializeComponent();
            tb.textChangedEv += textChangedEv;
        }

        private void textChangedEv(string newText)
        {
            name = tb.Text;
        }

        public event IWizard.updateDel update;

        public string getError()
        {
            return "";
        }

        public bool getIsValid()
        {
            return true;
        }

        public IWizard getNext()
        {
            return new Page1();
        }
    }
}
