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

namespace AbiCALC.Pages
{
    /// <summary>
    /// Interaction logic for page_add.xaml
    /// </summary>
    public partial class page_add : Page
    {
        //Attribute
        Dictionary<IName, Color> subjectColors = new Dictionary<IName, Color>();


        public page_add()
        {
            InitializeComponent();

            subjectColors[new testclass("Deutsch")] = new Color { R = 255, A = 255 };
            subjectColors[new testclass("Mathe")] = new Color { B = 255, A = 255 };

            subjectListXaml.getColor = (IName o) => { return subjectColors[o]; };
            subjectListXaml.GetPossibilties = () => { return new List<IName>(subjectColors.Keys); };

        }
    }
}
