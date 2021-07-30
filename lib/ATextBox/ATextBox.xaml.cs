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

namespace lib.ATextBox
{
    /// <summary>
    /// Interaction logic for textBox2.xaml
    /// </summary>
    public partial class ATextBox : UserControl
    {
        public delegate void textChangedDelegate(string newText);
        public event textChangedDelegate textChangedEv;
        public ATextBox()
        {
            InitializeComponent();
            GotFocus += TextBox2_GotFocus;
            LostFocus += TextBox2_LostFocus;
            TextBox2_LostFocus(null, null);
            tb.TextChanged += Tb_TextChanged;
        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            textChangedEv?.Invoke(tb.Text);
        }

        public string helpText;
        bool helpTextShown;


        private void setStyle(bool b)
        {
            tb.Style = b ? hintStyle : defaultStyle;
        }

        public void set(bool b)
        {
            helpTextShown = b;
            tb.Text = b ? helpText : string.Empty;
            setStyle(b);
        }

        private void TextBox2_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb.Text)) set(true);
        }

        private void TextBox2_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (helpTextShown) set(false);
        }

        public string HelpText
        {
            get => helpText;
            set
            {
                helpText = value;
                if (helpTextShown) set(true);
            }
        }
        public string Text
        {
            get => helpTextShown ? string.Empty : tb.Text;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    set(false);
                    tb.Text = value;
                }
            }
        }
        private Style s1, s2;
        public Style defaultStyle 
        {
            get 
            {
                return s1 ?? Application.Current.TryFindResource(typeof(TextBox)) as Style;
            }
            set 
            {
                s1 = value;
                setStyle(helpTextShown);
            }
        }

        public Style hintStyle
        {
            get
            {
                return s2 ?? defaultStyle ?? Application.Current.TryFindResource(typeof(TextBox)) as Style;
            }
            set
            {
                s2 = value;
                setStyle(helpTextShown);
            }
        }


        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ATextBox), new PropertyMetadata(null, new PropertyChangedCallback(textChanged)));

        private static void textChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ATextBox)d).Text = (string)e.NewValue;
        }

        public static readonly DependencyProperty HelpTextProperty =
            DependencyProperty.Register("HelpText", typeof(string), typeof(ATextBox), new PropertyMetadata(null, new PropertyChangedCallback(helpTextChanged)));

        private static void helpTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ATextBox)d).HelpText = (string)e.NewValue;
        }

        public static readonly DependencyProperty Style1Property =
           DependencyProperty.Register("defaultStyle", typeof(Style), typeof(ATextBox), new PropertyMetadata(null, new PropertyChangedCallback(s1c)));

        public static readonly DependencyProperty Style2Property =
           DependencyProperty.Register("hintStyle", typeof(Style), typeof(ATextBox), new PropertyMetadata(null, new PropertyChangedCallback(s2c)));

        private static void s1c(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ATextBox)d).defaultStyle = (Style)e.NewValue;
        }
        private static void s2c(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ATextBox)d).hintStyle = (Style)e.NewValue;
        }
    }
}
