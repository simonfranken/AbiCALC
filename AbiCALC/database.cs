using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    class database
    {
        data d = new data();
        static database singleton { get { return (_singleton ??= new database()); } }
        static database _singleton;

        public static void save() 
        {

        }

        public static data loadLast()
        {
            return singleton.d;
        }
    }
}
