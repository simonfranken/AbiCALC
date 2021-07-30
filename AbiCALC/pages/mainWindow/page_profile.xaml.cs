using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using lib.interfaces;

namespace AbiCALC.Pages.mainWindow
{
    /// <summary>
    /// Interaction logic for page_profile.xaml
    /// </summary>
    public partial class page_profile : Page
    {
        public page_profile()
        {
            //serialization.database.currentData.name.format = "Hallo, {0}!";
            InitializeComponent();
            DataContext = this;
            serialization.database.PropertyChangedStatic += test;
        }
        public observableItem<string> getName 
        {
            get => serialization.database.currentData != null ? serialization.database.currentData.Name : new observableItem<string>();
        }

        private void changeAcc(object sender, MouseButtonEventArgs e)
        {
            (new windows.changeAccount()).ShowDialog();
        }
        void test(object? sender, PropertyChangedEventArgs e)
        {
            NameField.GetBindingExpression(TextBlock.TextProperty).UpdateSource();
            NameField.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }
    }
}

