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
using AbiCALC.selections;

namespace AbiCALC.Pages.newAccount
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page, IWizard
    {
        selection s;
        Dictionary<string, bool?> d = new Dictionary<string, bool?> { { "", null }, { "Ja", true }, { "Nein", false } };
        public Page2(preSelection ps)
        {
            InitializeComponent();
            s = new selection(ps);
            setItemsSources(ps);
            createCallBacks();
        }

        private void setItemsSources(preSelection ps)
        {
            gwCombo.ItemsSource = Enum.GetValues(typeof(selection.GeoOrWrType));
            kmCombo.ItemsSource = Enum.GetValues(typeof(selection.KunstOrMusikType));
            reliCombo.ItemsSource = Enum.GetValues(typeof(selection.ReliType));
            gw12Combo.ItemsSource = d.Keys;
            lanCombo.ItemsSource = ps.lansPrimary;
            sciCombo.ItemsSource = Enum.GetValues(typeof(selection.ScienceType));
            extCombo.ItemsSource = s.getExtras().Keys;
            e12Combo.ItemsSource = d.Keys;
            gsCombo.ItemsSource = d.Keys;
        }

        private void createCallBacks()
        {
            wst.textChangedEv += textChangedEv;
            pst.textChangedEv += textChangedEv;
            gwCombo.SelectionChanged += Combo_SelectionChanged;
            kmCombo.SelectionChanged += Combo_SelectionChanged;
            reliCombo.SelectionChanged += Combo_SelectionChanged;
            gw12Combo.SelectionChanged += Combo_SelectionChanged;
            lanCombo.SelectionChanged += Combo_SelectionChanged;
            sciCombo.SelectionChanged += Combo_SelectionChanged;
            extCombo.SelectionChanged += Combo_SelectionChanged;
            e12Combo.SelectionChanged += Combo_SelectionChanged;
            gsCombo.SelectionChanged += Combo_SelectionChanged;
        }

        private void textChangedEv(string newText) => set();

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => set();

        private void set() 
        {
            if (gwCombo.SelectedItem != null) s.dp.gw = (selection.GeoOrWrType)gwCombo.SelectedItem;
            if (kmCombo.SelectedItem != null) s.dp.km = (selection.KunstOrMusikType)kmCombo.SelectedItem;
            if (reliCombo.SelectedItem != null) s.dp.rt = (selection.ReliType)reliCombo.SelectedItem;
            if (gw12Combo.SelectedItem != null) s.GeoWirtschaft12 = d[(string)gw12Combo.SelectedItem];
            if (lanCombo.SelectedItem != null) s.FremdsprachenIndex = (string)lanCombo.SelectedItem;
            if (sciCombo.SelectedItem != null) s.NaturWissenschaftsIndex = (int)((selection.ScienceType)sciCombo.SelectedItem);
            if (extCombo.SelectedItem != null) s.Index2 = (string)extCombo.SelectedItem;
            if (e12Combo.SelectedItem != null) s.Extra12 = d[(string)e12Combo.SelectedItem];
            if (gsCombo.SelectedItem != null) s.SoziGeschichteZsm = d[(string)gsCombo.SelectedItem];
            s.dp.PSemName = pst.Text;
            s.dp.WSemName = wst.Text;

            update?.Invoke();
        }

        public event IWizard.updateDel update;

        public string getError()
        {
            string e = "";
            s.isValid(ref e);
            return e;
        }

        public bool getIsValid()
        {
            string e = "";
            return s.isValid(ref e);
        }

        public IWizard getNext()
        {
            s.setSSIC();
            return new Page3(s);
        }
    }
}
