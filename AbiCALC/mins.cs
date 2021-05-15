using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class mins
    {
        private Dictionary<subjectTypes, int> m = new Dictionary<subjectTypes, int>();
        private List<abiexam> abis = new List<abiexam>();

        public mins(IEnumerable<abiexam> a, selection s) 
        {
            foreach(abiexam a2 in a)abis.Add(a2);
        }

        public int this[subjectTypes key]
        {
            get => GetValue(key);
        }

        private int GetValue(subjectTypes key)
        {
            bool b = false;
            foreach(abiexam a in abis)
                if(a.type == key)
                {
                    b = true;
                    break;
                }
            return b ? 4 : m[key];
        }
    }
}
