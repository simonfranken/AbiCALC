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

namespace AbiCALC.customUI.ListSelection
{
    /// <summary>
    /// Interaction logic for listSelection.xaml
    /// </summary>
    public partial class listSelection : UserControl
    {
        List<string> options;
        string selectedOption;
        Color defaultColor;


        Border selectedBorder
        {
            get => _selectedBorder;

            set
            {
                if (_selectedBorder != null) _selectedBorder.Background = new SolidColorBrush(defaultColor);
                _selectedBorder = value;
                selectedOption = ((TextBlock)_selectedBorder.Child).Text;
                _selectedBorder.Background = new SolidColorBrush(new Color { R = 255, G = 255, B = 255 });
            }
        }
        Border _selectedBorder;
        

        public listSelection()
        {
            InitializeComponent();
            itemcontrol.ItemsSource = options;
            defaultColor = new Color { R = 26, G = 26, B = 26, A = 255 };
        }

        private void borderClicked(object sender, MouseButtonEventArgs e)
        {
            selectedBorder = (Border)sender;
        }
    }
}
