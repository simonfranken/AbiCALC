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

        public void SetCollection(ObservableCollection<IName> newCollectionInput) => itemcontrol.ItemsSource = newCollectionInput != null ? newCollectionInput : new List<IName>();

        public AListSelection()
        {

            InitializeComponent();
        }

        private Border _selectedBorder;

        private Border SelectedBorder
        {
            get => _selectedBorder;

            set
            {
                if (_selectedBorder != null) _selectedBorder.Background = new SolidColorBrush(defaultColor);
                _selectedBorder = value;
                if(_selectedBorder != null) _selectedBorder.Background = new SolidColorBrush(getColor != null ? getColor(GetSelected()) : new Color { R = 255, G = 255, B = 0, A = 255 }) ;
                SelectionChanged?.Invoke();
            }
        }

        public IName GetSelected() 
        {
            return SelectedBorder != null ? GetSource(SelectedBorder) : null;
        }

        private static IName GetSource(Border b) 
        {
            return (IName)((TextBlock)(b.Child)).GetBindingExpression(TextBlock.TextProperty)?.DataItem;
        }

        private Border GetBorder(IName n) 
        {
            itemcontrol.UpdateLayout();
            var x  = itemcontrol.ItemContainerGenerator.ContainerFromItem(n) as Border;
            return x;
        }


        private void BorderClicked(object sender, MouseButtonEventArgs e) => SelectedBorder = (Border)sender;

        private void MouseOver(object sender, DependencyPropertyChangedEventArgs e)
        {
            var border = (Border)sender;

            border.Background = border.IsMouseOver switch
            {
                true when border != SelectedBorder => (SolidColorBrush) FindResource("color_midgrey"),
                false when border != SelectedBorder => (SolidColorBrush) FindResource("color_darkgrey"),
                _ => border.Background
            };
        }
        
        public void updateColor() => SelectedBorder = SelectedBorder;
        
    }
}
