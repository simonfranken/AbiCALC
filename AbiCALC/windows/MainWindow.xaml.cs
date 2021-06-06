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
using AbiCALC.Pages;
using AbiCALC.Pages.mainWindow;

namespace AbiCALC.windows
{
    public partial class MainWindow : Window, IWindow
    {

        //Attributes
        List<Grid> windows = new List<Grid>();
        Dictionary<IName, Color> subjectColors = new Dictionary<IName, Color>();
        Dictionary<Image, Type> pagesType = new Dictionary<Image, Type>();
        Dictionary<Type, Page> pages = new Dictionary<Type, Page>();

        public static MainWindow singleton { get => _singleton; }
        private static MainWindow _singleton = null;

        private Page this[Type key]
        {
            get 
            {
                return pages[key] ??= ((Page)Activator.CreateInstance(key));
            }
        }

        public void set(Type t) 
        {
            windowFrame.Content = this[t];
        }

        //Main-Method
        public MainWindow()
        {
            InitializeComponent();
            _singleton ??= this;
            pagesType[home_icon] = typeof(page_home);
            pagesType[add_icon] = typeof(page_add);
            pagesType[profile_icon] = typeof(page_profile);
            pagesType[settings_icon] = typeof(page_settings);

            foreach (Type i in pagesType.Values) pages[i] = null;
            pages[typeof(page_table)] = null;
            set(typeof(page_home));
        }

        //Events
        public void close_clicked(object sender, MouseButtonEventArgs e)
        {
            Close();
            ((App)App.Current).close();
        }
        public void max_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        public void min_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void pageSwitched(object sender, MouseButtonEventArgs e)
        {
            set(pagesType[(Image)sender]);
        }
    }
}
