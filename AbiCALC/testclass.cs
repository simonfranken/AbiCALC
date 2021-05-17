using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    class testclass : IName
    {
        private string s = string.Empty;
        public string Name => s;

        public testclass(string _s) 
        {
            s = _s;
        }
    }
}
