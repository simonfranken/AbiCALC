using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    class data
    {
        semester[] semesters = new semester[4];

        public data()
        {
            for (int i = 0; i < semesters.Length; i++)
            {
                semesters[i] = new semester();
            }
        }
    }
}
