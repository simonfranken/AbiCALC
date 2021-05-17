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
using System.Windows.Controls.Primitives;
using AbiCALC.customUI.ListSelection;

namespace AbiCALC
{
    public partial class MainWindow : Window
    {

        //Attributes
        List<Grid> windows = new List<Grid>();
        Dictionary<subjectTypes, Brush> colorDict = new Dictionary<subjectTypes, Brush>();
        List<subjectTypes> subjectList;
        Border selectedSubjectBorder
        {
            get => _selectedSubjectBorder;
            set
            {
                if (_selectedSubjectBorder != null) _selectedSubjectBorder.Background = new SolidColorBrush(new Color { R = 26, G = 26, B = 26 , A = 255});
                _selectedSubjectBorder = value;
                TextBlock textBlock = (TextBlock)_selectedSubjectBorder.Child;
                selectedSubject = (subjectTypes)Enum.Parse(typeof(subjectTypes), textBlock.Text, true);
                if (colorDict.ContainsKey(selectedSubject)) _selectedSubjectBorder.Background = colorDict[selectedSubject];
                else _selectedSubjectBorder.Background = new SolidColorBrush(new Color { R = 255, G = 255, B = 255, A = 255 });
            }
        }
        Border _selectedSubjectBorder;
        subjectTypes selectedSubject;

        //Main-Method
        public MainWindow()
        {
            InitializeComponent();
            initialize_colorDict();
            initialize_subjectList();
            initialize_windows();

            subjectListXaml.getColor = (IName o) => { return new Color { R = 0, G = 100, B = 100, A = 255 }; };
            subjectListXaml.GetPossibilties = () => { return new List<IName> {new testclass("a"), new testclass("b"), new testclass("c") }; };
        }

        //Methods
        private void initialize_windows()
        {
            windows.Add(home_window);
            windows.Add(profile_window);
            windows.Add(add_window);
        }
        private void hide_all_windows()
        {
            foreach (Grid grid in windows)
            {
                grid.Visibility = Visibility.Hidden;
            }
        }
        private void initialize_colorDict()
        {
            colorDict.Add(subjectTypes.Deutsch, new SolidColorBrush(new Color { R = 0, G = 128, B = 255, A = 255 }));
            colorDict.Add(subjectTypes.Informatik, new SolidColorBrush(new Color { R = 255, G = 51, B = 51, A = 255 }));
            colorDict.Add(subjectTypes.Mathe, new SolidColorBrush(new Color { R = 255, G = 255, B = 51, A = 255 }));
        }
        private void initialize_subjectList()
        {
            subjectList = new List<subjectTypes>();
            subjectList.Add(subjectTypes.Deutsch);
            subjectList.Add(subjectTypes.Mathe);
            subjectList.Add(subjectTypes.Englisch);
            subjectList.Add(subjectTypes.Franzoesisch);
            subjectList.Add(subjectTypes.Informatik);

        }


        //Events
        private void close_clicked(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void max_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        private void min_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void add_icon_clicked(object sender, MouseButtonEventArgs e)
        {
            hide_all_windows();
            add_window.Visibility = Visibility.Visible;

        }
        private void home_icon_clicked(object sender, MouseButtonEventArgs e)
        {
            hide_all_windows();

            home_window.Visibility = Visibility.Visible;
        }
        private void profile_icon_clicked(object sender, MouseButtonEventArgs e)
        {
            hide_all_windows();

            profile_window.Visibility = Visibility.Visible;
        }
        private void subjecttype_selected(object sender, MouseButtonEventArgs e)
        {
            selectedSubjectBorder = (Border)sender;
        }
    }
}
