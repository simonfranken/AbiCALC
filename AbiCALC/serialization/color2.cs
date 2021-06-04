using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AbiCALC.serialization
{
    [Serializable]
    public class color2
    {
        public color2(Color cn) 
        {
            color = cn;
        }
        public Color color 
        {
            get => c;
            set => c = value;
        }
        private byte r, g, b;
        [NonSerialized]
        private Color c;
        [OnDeserialized]
        public void deserialized(StreamingContext context) 
        {
            color = new Color {R=r, G=g, B=b , A=255};
        }
        [OnSerializing]
        public void serializing(StreamingContext context)
        {
            r = color.R;
            g = color.G;
            b = color.B;
        }

        public static implicit operator color2(Color cn) 
        {
            return new color2(cn);
        }
    }
}
