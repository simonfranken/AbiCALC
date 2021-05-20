using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AbiCALC
{
    class database
    {
        const string ApplicationName = "AbiCalc2";
        static DirectoryInfo dirAppData = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),ApplicationName));
        //static FileInfo file = new FileInfo("");

        data d = new data();

        static database singleton { get { return (_singleton ??= new database()); } }
        static database _singleton;

        public static void save() 
        {

        }

        public static data current
        {
            get => singleton.d;
        }
    }
}
