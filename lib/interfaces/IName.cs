using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.interfaces
{
    public interface IName
    {
        observableItem<string> Name
        {
            get;
        }
    }
}
