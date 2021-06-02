using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Color defaultColor= new Color { R = 26, G = 26, B = 26, A = 255 };

        public delegate Color getColorDelegate(IName o);
        public getColorDelegate getColor = null;

        public delegate void selectionChangedEventHandler();
        public event selectionChangedEventHandler selectionChanged;

        public void setCollection(ObservableCollection<IName> newCollectionInput) => itemcontrol.ItemsSource = newCollectionInput != null ? newCollectionInput : new List<IName>();

        public listSelection()
        {

            InitializeComponent();
        }

        Border _selectedBorder;
        Border selectedBorder
        {
            get => _selectedBorder;

            set
            {
                if (_selectedBorder != null) _selectedBorder.Background = new SolidColorBrush(defaultColor);
                _selectedBorder = value;
                _selectedBorder.Background = new SolidColorBrush(getColor != null ? getColor(getSelected()) : new Color { R = 255, G = 255, B = 0, A = 255 }) ;
                selectionChanged?.Invoke();
            }
        }
        
        public IName getSelected() 
        {
            return selectedBorder != null ? (IName)((TextBlock)(_selectedBorder.Child)).GetBindingExpression(TextBlock.TextProperty).DataItem : null;
        }


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
