using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using lib.interfaces;

namespace AbiCALC.windows
{
    /// <summary>
    /// Interaction logic for colorPicker.xaml
    /// </summary>
    public partial class colorPicker : Window, IWindow
    {
        Dictionary<Slider, TextBox> dict = new Dictionary<Slider, TextBox>();
        Dictionary<TextBox, Slider> dict2 = new Dictionary<TextBox, Slider>();
        public colorPicker()
        {
            InitializeComponent();
            rT.Text = "0";
            gT.Text = "0";
            bT.Text = "0";
            dict[rS] = rT;
            dict[gS] = gT;
            dict[bS] = bT;
            foreach (Slider s in dict.Keys)
            {
                dict2[dict[s]] = s;
            }
        }
        public colorPicker(Color c) : this() 
        {
            rT.Text = c.R + "";
            gT.Text = c.G + "";
            bT.Text = c.B + "";
            foreach (TextBox item in dict2.Keys) textUpdated(item, null);
        }

        private void sliderUpdated(object sender, DragDeltaEventArgs e)
        {
            dict[((Slider)sender)].Text = (int)(((Slider)sender).Value) + "";
            update();
        }

        private void update() 
        {
            preview.Background = new SolidColorBrush(new Color {R = (byte)rS.Value, G = (byte)gS.Value , B = (byte)bS.Value , A = 255});
        }
        private void textUpdated(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string s = t.Text;
            int? i = null;
            bool b = isValid(s, ref i);
            if(b) 
            {
                dict2[t].Value = (int)i;
            }
            else 
            {
                t.Text = "0";
                t.CaretIndex = t.Text.Length;
            }
            update();
        }

        private void textFinished(object sender, KeyboardFocusChangedEventArgs e) 
        {
            TextBox t = (TextBox)sender;
            t.Text = (int)(dict2[t].Value) + "";
            update();
        }

        private bool isValid(string input, ref int?  output) 
        {
            bool b = int.TryParse(input, out int i);
            if (b) output = minMax(i);
            return b;
        }

        private int minMax(int i) 
        {
            return i < 0 ? 0 : (i > 255 ? 255 : i);
        }
        public void close_clicked(object sender, MouseButtonEventArgs e)
        {
            App.selectedColor = ((SolidColorBrush)preview.Background).Color;
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

        private void Button_Click(object sender, MouseButtonEventArgs e) => close_clicked(sender, e);
    }
}
