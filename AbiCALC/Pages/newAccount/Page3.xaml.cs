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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page, IWizard
    {
        selections.selection s;
        public Page3(selections.selection _s)
        {
            InitializeComponent();
            s = _s;

            c1.ItemsSource = s.getAbiPos()[0];
            c2.ItemsSource = s.getAbiPos()[1];
            c3.ItemsSource = s.getAbiPos()[2];
            c4.ItemsSource = s.getAbiPos()[3];
            c5.ItemsSource = s.getAbiPos()[4];

            c1.SelectionChanged += selectionChanged;
            c2.SelectionChanged += selectionChanged;
            c3.SelectionChanged += selectionChanged;
            c4.SelectionChanged += selectionChanged;
            c5.SelectionChanged += selectionChanged;
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e) => set();

        public void set() 
        {
            update?.Invoke();
        }

        public event IWizard.updateDel update;

        public string getError()
        {
            string e = "";
            s.abisValid(get(), ref e);
            return e;
        }

        public List<subjectTypes> get() 
        {
            List<subjectTypes> ret = new List<subjectTypes>();

            ret.Add(c1.SelectedItem != null ? (subjectTypes)c1.SelectedItem : null);
            ret.Add(c2.SelectedItem != null ? (subjectTypes)c2.SelectedItem : null);
            ret.Add(c3.SelectedItem != null ? (subjectTypes)c3.SelectedItem : null);
            ret.Add(c4.SelectedItem != null ? (subjectTypes)c4.SelectedItem : null);
            ret.Add(c5.SelectedItem != null ? (subjectTypes)c5.SelectedItem : null);

            return ret;
        }

        public bool getIsValid()
        {
            string e = "";
            return s.abisValid(get(), ref e);
        }

        public IWizard getNext()
        {
            s.ssic.update(createAbis(get()));
            return null;
        }

        public List<abiexam> createAbis(List<subjectTypes> sts) 
        {
            List<abiexam> ret = new List<abiexam>();

            foreach (subjectTypes st in sts)
            {
                ret.Add(new abiexam(st));
            }

            return ret;
        }
    }
}
