using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
        public static Color selected;
        public App() 
        {
            InitializeComponent();
        }
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
            ((IWindow)Window.GetWindow((Image)sender)).min_clicked(sender, e);
        }
        public void close() 
        {
            foreach (Window x in Windows) x.Close();
            database.close();
            Shutdown();
        }
        public ObservableCollection<subjectTypes> subjects;

        public ObservableCollection<semester> semesters;

        public data promptNewAccount(int i) 
        {
            return new data(new selection(), i);
            //throw new NotImplementedException();
        }
    }
}
