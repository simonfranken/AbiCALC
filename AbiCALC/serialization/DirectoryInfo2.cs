using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.serialization
{
    public class DirectoryInfo2
    {
        private DirectoryInfo _dir = null;
        
        public DirectoryInfo2(string s) 
        {
            _dir = new DirectoryInfo(s);
        }
        public DirectoryInfo dir
        {
            get 
            {
                if(_dir != null) 
                {
                    if (!_dir.Exists) _dir.Create();
                    return _dir;
                }
                return null;
            }
        }

        public static explicit operator DirectoryInfo2(DirectoryInfo dIn) 
        {
            return new DirectoryInfo2(dIn.FullName);
        }
    }
}
