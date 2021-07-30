using System.Collections.ObjectModel;
using System.Windows;
using lib.interfaces;
using System.Windows.Input;
using System.Windows.Controls;
using System.Drawing;
using AbiCALC;

namespace AbiCALC.resourceDictionaries
{
    public partial class styles : ResourceDictionary
    {


        public styles()
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
            ((IWindow) Window.GetWindow((Image)sender)).min_clicked(sender, e);
        }
        
        public ObservableCollection<subjectTypes> subjects;

        public ObservableCollection<semester> semesters;

        
        
    }
}