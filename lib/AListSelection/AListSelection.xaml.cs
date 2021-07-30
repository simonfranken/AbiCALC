using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using lib.interfaces;

namespace lib.AListSelection
{
    /// <summary>
    /// Interaction logic for listSelection.xaml
    /// </summary>
    public partial class AListSelection : UserControl
    {
        Color defaultColor= new Color { R = 26, G = 26, B = 26, A = 255 };

        public delegate Color GetColorDelegate(IName o);
        public GetColorDelegate getColor = null;

        public delegate void SelectionChangedEventHandler();
        public event SelectionChangedEventHandler SelectionChanged;

        public void setCollection(ObservableCollection<IName> newCollectionInput) => itemcontrol.ItemsSource = newCollectionInput != null ? newCollectionInput : new List<IName>();

        public AListSelection()
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
                if(_selectedBorder != null) _selectedBorder.Background = new SolidColorBrush(getColor != null ? getColor(getSelected()) : new Color { R = 255, G = 255, B = 0, A = 255 }) ;
                SelectionChanged?.Invoke();
            }
        }
        
        public IName getSelected() 
        {
            return selectedBorder != null ? getSource(selectedBorder) : null;
        }

        private IName getSource(Border b) 
        {
            return (IName)((TextBlock)(b.Child)).GetBindingExpression(TextBlock.TextProperty).DataItem;
        }

        private Border getBorder(IName n) 
        {
            itemcontrol.UpdateLayout();
            var x  = itemcontrol.ItemContainerGenerator.ContainerFromItem(n) as Border;
            return x;
        }


        private void borderClicked(object sender, MouseButtonEventArgs e) => selectedBorder = (Border)sender;

        private void mouseOver(object sender, DependencyPropertyChangedEventArgs e)
        {
            Border border = (Border)sender;

            if (border.IsMouseOver && border != selectedBorder) border.Background = (SolidColorBrush)FindResource("color_midgrey");

            if (!border.IsMouseOver && border != selectedBorder) border.Background = (SolidColorBrush)FindResource("color_darkgrey");

        }

        public void updateColor() => selectedBorder = selectedBorder;
    }
}
