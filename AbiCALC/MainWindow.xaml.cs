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
        Dictionary<IName, Color> subjectColors = new Dictionary<IName, Color>();

        //Main-Method
        public MainWindow()
        {
            InitializeComponent();
            initialize_windows();

            subjectColors[new testclass("Deutsch")] = new Color { R = 255, A = 255 };
            subjectColors[new testclass("Mathe")] = new Color { B = 255, A = 255 };

            subjectListXaml.getColor = (IName o) => { return subjectColors[o]; };
            subjectListXaml.GetPossibilties = () => { return new List<IName>(subjectColors.Keys); };
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
    }
}
