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
        Dictionary<string, bool?> d = new Dictionary<string, bool?> { { "", null }, { "Ja", true }, {"Nein", false } };
        Dictionary<string, bool?> d2 = new Dictionary<string, bool?> { { "", null }, { "1", true }, {"2", false } };
        public Page1()
        {
            InitializeComponent();
            zweigeCombo.ItemsSource = Enum.GetValues(typeof(preSelection.GymType));
            abwCombo.ItemsSource = d.Keys;
            abw2Combo.ItemsSource = d2.Keys;
            abwCombo.SelectedIndex = 0;
            abw2Combo.SelectedIndex = 0;
            addCallbacks();
            set();
        }

        private void addCallbacks()
        {
            l1.textChangedEv += textChanged;
            l2.textChangedEv += textChanged;
            l3.textChangedEv += textChanged;
            ls.textChangedEv += textChanged;
            lw.textChangedEv += textChanged;
            zweigeCombo.SelectionChanged += Combo_SelectionChanged;
            abwCombo.SelectionChanged += Combo_SelectionChanged;
            abw2Combo.SelectionChanged += Combo_SelectionChanged;
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => set();

        private void textChanged(string newText) => set();

        private void set()
        {
            ps.lan1 = l1.Text;
            ps.lan2 = l2.Text;
            ps.lan3 = l3.Text;
            ps.lanLate = ls.Text;
            ps.lanW = lw.Text;
            if (zweigeCombo.SelectedItem != null) ps.g = (preSelection.GymType)zweigeCombo.SelectedItem;
            else ps.g = null;
            ps.useLanLate = d[(string)abwCombo.SelectedItem];
            ps.replaceLan1 = d2[(string)abw2Combo.SelectedItem];
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

        public IWizard getNext() 
        {
            return new Page2(ps);
        }
    }
}
