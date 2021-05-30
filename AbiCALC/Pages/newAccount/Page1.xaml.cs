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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page, IWizard
    {
        preSelection ps = new preSelection();
        public Page1()
        {
            InitializeComponent();
            zweigeCombo.ItemsSource = Enum.GetValues(typeof(preSelection.GymType));
            l1.textChangedEv += textChanged;
            l2.textChangedEv += textChanged;
            l3.textChangedEv += textChanged;
            ls.textChangedEv += textChanged;
            lw.textChangedEv += textChanged;
            zweigeCombo.SelectionChanged += ZweigeCombo_SelectionChanged;
            textChanged("");
        }

        private void ZweigeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) => set();

        private void textChanged(string newText) => set();

        private void set()
        {
            ps.lan1 = l1.Text;
            ps.lan2 = l2.Text;
            ps.lan3 = l3.Text;
            ps.lanLate = ls.Text;
            ps.lanW = lw.Text;
            if(zweigeCombo.SelectedItem != null) ps.g = (preSelection.GymType)zweigeCombo.SelectedItem;
            update?.Invoke();
        }

        public string getError()
        {
            string s = "";
            ps.isValid(ref s);
            return s;
        }

        public bool getIsValid()
        {
            string s = "";
            return ps.isValid(ref s);
        }

        public event IWizard.updateDel update;
    }
}
