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
using System.Windows.Shapes;

namespace AbiCALC.windows
{
    /// <summary>
    /// Interaction logic for editSubjectType.xaml
    /// </summary>
    public partial class editSubjectType : Window, IWindow
    {
        public editSubjectType()
        {
            InitializeComponent();
            listSubjects.setCollection(new System.Collections.ObjectModel.ObservableCollection<IName>(serialization.database.currentData.getSubjectTypes()));
            listSubjects.getColor += test;
            nameTextBox.TextChanged += NameTextBox_TextChanged;
            listSubjects.selectionChanged += ListSubjects_selectionChanged;
        }

        public Color test(IName i) 
        { 
            return i != null ? ((subjectTypes) i).c.color : new Color(); 
        }

        private void ListSubjects_selectionChanged()
        {
            subjectTypes x = ((subjectTypes)listSubjects.getSelected());
            if(x != null) nameTextBox.Text = x.Name.itemValue;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            var x = ((subjectTypes)listSubjects.getSelected());
            if(x != null) x.Name.itemValue = nameTextBox.Text;
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

        private void fertigClicked(object sender, MouseButtonEventArgs e) => close_clicked(null, null);

        private void changeColor(object sender, MouseButtonEventArgs e)
        {
            new colorPicker(((subjectTypes)listSubjects.getSelected()).c.color).ShowDialog();
            if(App.selectedColor != null) 
            {
                ((subjectTypes)listSubjects.getSelected()).c.color = (Color)App.selectedColor;
                App.selectedColor = null;
                listSubjects.updateColor();
            }
        }
    }
}
