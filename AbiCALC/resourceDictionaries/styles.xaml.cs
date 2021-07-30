using System.Collections.ObjectModel;
using System.Windows;
using lib.interfaces;
using System.Windows.Input;
using System.Windows.Controls;
using System.Drawing;

namespace AbiCALC.resourceDictionaries
{
    public partial class styles : ResourceDictionary
    {


        public styles()
        {
            InitializeComponent();
        }
        
        public static Color? selectedColor = null;
        public static selections.selection newSelection;
        public static bool newAccountFinished = false;
        private void close_clicked(object sender, MouseButtonEventArgs e)
        {
            ((IWindow)Window.GetWindow((Image)sender)).close_clicked(sender, e);
        }
        private void max_clicked(object sender, MouseButtonEventArgs e)
        {
            ((IWindow)Window.GetWindow((Image)sender)).max_clicked(sender, e);
        }
        private void min_clicked(object sender, MouseButtonEventArgs e)
        {
            ((IWindow) Window.GetWindow((Image)sender)).min_clicked(sender, e);
        }
        public void Close() 
        {
            foreach (Window x in Windows) x.Close();
            serialization.database.close();
            Shutdown();
        }
        public ObservableCollection<subjectTypes> subjects;

        public ObservableCollection<semester> semesters;

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
        
    }
}