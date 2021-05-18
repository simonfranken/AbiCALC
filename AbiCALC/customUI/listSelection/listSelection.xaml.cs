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
        Color defaultColor;

        public void refresh() 
        {
            itemcontrol.ItemsSource = GetPossibilties != null ? GetPossibilties() : new List<IName>();
        }

        public delegate List<IName> getPossibiltiesDelegate();
        public delegate Color getColorDelegate(IName o);
        private getPossibiltiesDelegate getPossibilties = null;
        public getColorDelegate getColor = null;

        public listSelection()
        {

            InitializeComponent();
            defaultColor = new Color { R = 26, G = 26, B = 26, A = 255 };
            refresh();
        }

        Border selectedBorder
        {
            get => _selectedBorder;

            set
            {
                if (_selectedBorder != null) _selectedBorder.Background = new SolidColorBrush(defaultColor);
                _selectedBorder = value;
                _selectedBorder.Background = new SolidColorBrush(getColor != null ? getColor((IName)((TextBlock)(_selectedBorder.Child)).GetBindingExpression(TextBlock.TextProperty).DataItem) : new Color { R = 255, G = 255, B = 0, A = 255 }) ;
            }
        }

        public getPossibiltiesDelegate GetPossibilties 
        {
            get => getPossibilties; 
            set 
            {
                getPossibilties = value;
                refresh();
            }
        }

        Border _selectedBorder;
        


        private void borderClicked(object sender, MouseButtonEventArgs e)
        {
            selectedBorder = (Border)sender;
        }

        private void mouseOver(object sender, DependencyPropertyChangedEventArgs e)
        {
            Border border = (Border)sender;

            if (border.IsMouseOver && border != selectedBorder) border.Background = (SolidColorBrush)FindResource("color_midgrey");

            if (!border.IsMouseOver && border != selectedBorder) border.Background = (SolidColorBrush)FindResource("color_darkgrey");

        }
    }
}
