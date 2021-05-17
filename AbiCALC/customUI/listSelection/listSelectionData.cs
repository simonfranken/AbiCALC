using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AbiCALC.customUI.ListSelection
{

    class listSelectionData<T>
    {
        T selectedOption;
        public delegate (int R, int G, int B) color(T selectedOption);
        color GetColor;



        Border selectedBorder
        {
            get => _selectedBorder;

            set
            {
            }
        }
        Border _selectedBorder;
        List<T> options;





        public listSelectionData(color c)
        {
            GetColor = c;
        }

        private Brush getBrush(T t)
        {
            (int r, int g, int b) rgb = GetColor(t);
            return new SolidColorBrush(new Color { R = (byte)rgb.r, G = (byte)rgb.g, B = (byte)rgb.b, A = 255 });
        }
    }
}
