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

namespace AbiCALC.Pages.mainWindow
{
    /// <summary>
    /// Interaction logic for page_settings.xaml
    /// </summary>
    public partial class page_settings : Page
    {
        public page_settings()
        {
            InitializeComponent();
        }

        private void color_change (object sender, MouseButtonEventArgs e)
        {
            (new windows.editSubjectType()).ShowDialog();
        }

        private void name_change (object sender, MouseButtonEventArgs e)
        {
            nameField.Visibility = Visibility.Visible;
        }

        private void name_change_ok (object sender, MouseButtonEventArgs e)
        {
            serialization.database.currentData.Name.itemValue = nameInput.Text;
            nameField.Visibility = Visibility.Hidden;
            nameInput.Text = "";
        }
    }
}
