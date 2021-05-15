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

namespace AbiCALC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Attributes
        List<Grid> windows = new List<Grid>();
        Border _selectedSubject = null;
        Border selectedSubject
        {
            get => _selectedSubject;
            set
            {
                if (selectedSubject != null) selectedSubject.Background = new SolidColorBrush(new Color { R = 26, G = 26, B = 26, A = 255 });
                _selectedSubject = value;
                TextBlock textBlock = (TextBlock)selectedSubject.Child;
                if (colorDict.ContainsKey(textBlock.Text)) selectedSubject.Background = colorDict[textBlock.Text];
                else selectedSubject.Background = new SolidColorBrush(new Color { R = 255, G = 127, B = 0, A = 255 });
            }
        }
        Dictionary<string, Brush> colorDict = new Dictionary<string, Brush>();

        //Main-Method
        public MainWindow()
        {
            InitializeComponent();
            initialize_colorDict();
            windows.Add(home_window);
            windows.Add(profile_window);
            windows.Add(add_window);
        }


        //Methods
        private void hide_all_windows()
        {
            foreach (Grid grid in windows)
            {
                grid.Visibility = Visibility.Hidden;
            }
        }
        private void initialize_colorDict()
        {
            colorDict.Add("Deutsch", new SolidColorBrush(new Color { R = 0, G = 128, B = 255, A = 255 }));
            colorDict.Add("Mathematik", new SolidColorBrush(new Color { R = 255, G = 51, B = 51, A = 255 }));
            colorDict.Add("Înformatik", new SolidColorBrush(new Color { R = 255, G = 255, B = 51, A = 255 }));
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
            selectedSubject = (Border)sender;
        }


    }
}
