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
        List<Grid> windows = new List<Grid>();

        /*
        private int navMin = 50, navMax = 150;
        
        private bool navPanelMovementFinished = true;
        private bool navPanelMovementAim = true;

        private object navPanelMovementFinishedLock = new object();
        private object navPanelMovementAimLock = new object();
        */
        public MainWindow()
        {
            InitializeComponent();
            windows.Add(home_window);
            windows.Add(profile_window);
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

    }
}
