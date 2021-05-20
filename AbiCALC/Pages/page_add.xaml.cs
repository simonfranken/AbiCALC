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
        Dictionary<IName, Color> semesterColors = new Dictionary<IName, Color>();


        public page_add()
        {
            InitializeComponent();
            initSubjectSelection();
            initSemesterSelection();
        }

        private void initSubjectSelection()
        {
            subjectColors[new testclass("Deutsch")] = ((SolidColorBrush)FindResource("color_red")).Color;
            subjectColors[new testclass("Mathe")] = ((SolidColorBrush)FindResource("color_blue")).Color;

            subjectSelection.getColor = (IName o) => { return subjectColors[o]; };
            subjectSelection.GetPossibilties = () => { return new List<IName>(subjectColors.Keys); };
        }

        private void initSemesterSelection()
        {
            semesterColors[new testclass("11/1")] = ((SolidColorBrush)FindResource("color_violet")).Color;
            semesterColors[new testclass("11/2")] = ((SolidColorBrush)FindResource("color_violet")).Color;
            semesterColors[new testclass("12/1")] = ((SolidColorBrush)FindResource("color_violet")).Color;
            semesterColors[new testclass("12/2")] = ((SolidColorBrush)FindResource("color_violet")).Color;

            semesterSelection.getColor = (IName o) => { return semesterColors[o]; };
            semesterSelection.GetPossibilties = () => { return new List<IName>(semesterColors.Keys); };
        }
    }
}
