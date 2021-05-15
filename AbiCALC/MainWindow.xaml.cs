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
using System.ComponentModel;

namespace AbiCALC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Grid> windows = new List<Grid>();
        data d = new data();

        public string getNameString 
        {
            get => $"Hallo {name}!";
        }

        public string getGrade 
        {
            get => lookUpTable.getAbiGrade(d.getPoints());
        }

        public string name 
        {
            get => d.name;           
        }

        public MainWindow()
        {
            InitializeComponent();
            windows.Add(home_window);
            windows.Add(profile_window);
            windows.Add(add_window);
            this.DataContext = this;
            d = database.loadLast();
        }

        private void hide_all_windows()
        {
            foreach (Grid grid in windows)
            {
                grid.Visibility = Visibility.Hidden;
            }
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

        private void subjecttype_selected(object sender, MouseButtonEventArgs e)
        {
        }

        protected override void OnClosing(CancelEventArgs e) 
        {
            database.save();
        }
    }
}
