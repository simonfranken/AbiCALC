using lib.interfaces;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace AbiCALC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
        }
        
        
        public static Color? selectedColor = null;
        public static selections.selection newSelection;
        public static bool newAccountFinished = false;
        
        private static data promptNewAccount() 
        {
            while(!newAccountFinished)
                (new windows.newAccount()).ShowDialog();
            newAccountFinished = false;
            return new data(newSelection);
        }

        public static void createNewAccount() 
        {
            serialization.database.addNew(promptNewAccount());
        }
        public void Close() 
        {
            foreach (Window x in Application.Current.Windows) x.Close();
            serialization.database.close();
            Application.Current.Shutdown();
        }
        
    }
}
