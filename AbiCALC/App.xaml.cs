﻿using lib.interfaces;
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
        public static Color? selectedColor = null;
        public static selections.selection newSelection;
        public static bool newAccountFinished = false;
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
