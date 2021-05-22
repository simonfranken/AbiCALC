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

namespace AbiCALC.customUI.textBoxWithBackground
{
    /// <summary>
    /// Interaction logic for textBoxWithBackground.xaml
    /// </summary>
    public partial class textBoxWithBackground : UserControl
    {
        public textBoxWithBackground()
        {
            InitializeComponent();
            DataContext = this;
            var x = this.GetValue(backgroundContentProperty);
        }


        public UIElement backgroundContent
        {
            get { return (UIElement)GetValue(backgroundContentProperty); }
            set 
            {
                SetValue(backgroundContentProperty, value);
                Style style = this.FindName("mainStyle") as Style;
                VisualBrush x = style.Resources["bg"] as VisualBrush;
                ContentPresenter s = x.Visual as ContentPresenter;
                s.Content = value;
            }
        }

        // Using a DependencyProperty as the backing store for tooltip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty backgroundContentProperty =
            DependencyProperty.Register("backgroundContent", typeof(UIElement), typeof(textBoxWithBackground), new PropertyMetadata(null, new PropertyChangedCallback(test)));

        private static void test(DependencyObject d, DependencyPropertyChangedEventArgs e) 
        {
            ((textBoxWithBackground)d).backgroundContent = (UIElement)e.NewValue;
        }

    }
}
