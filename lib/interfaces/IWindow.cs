using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lib.interfaces
{
    interface IWindow
    {
        void close_clicked(object sender, MouseButtonEventArgs e);
        void max_clicked(object sender, MouseButtonEventArgs e);
        void min_clicked(object sender, MouseButtonEventArgs e);
    }
}
