using System.Windows;
using lib.interfaces;
using System.Windows.Input;
using System.Windows.Media;

namespace AbiCALC.windows
{
    /// <summary>
    /// Interaction logic for newAccount.xaml
    /// </summary>
    public partial class newAccount : Window, IWindow
    {
        Pages.newAccount.IWizard x;
        public newAccount()
        {
            InitializeComponent();
            DataContext = this;
            _hc.itemValue = new SolidColorBrush( new Color {R = 255, G = 127, B = 0, A = 255});
            next(new Pages.newAccount.Page0());
        }

        public void next(Pages.newAccount.IWizard n) 
        {
            if(x != null) x.update -= updateLoc;
            if(n == null) 
            {
                finish();
                return;
            }
            x = n;
            windowFrame.Content = x;
            x.update += updateLoc;
            updateLoc();
        }

        private void finish()
        {
            App.newSelection.name = Pages.newAccount.Page0.name;
            close_clicked(null, null);
        }

        private void updateLoc()
        {
            errorText.Text = x.getError();
            _hc.itemValue = new SolidColorBrush( x.getIsValid() ? new Color { R = 255, G = 127, B = 0 , A = 255} : new Color { R = 255, A = 255});
        }

        public void close_clicked(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        public void max_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        public void min_clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ok_clicked(object sender, MouseButtonEventArgs e)
        {
            if (x.getIsValid()) next(x.getNext());
        }
        private observableItem<SolidColorBrush> _hc = new observableItem<SolidColorBrush>();

        public observableItem<SolidColorBrush> hoverColor 
        {
            get => _hc;
        }
    }
}
