using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    class database
    {
        semester[] semesters = new semester[4];

        public database()
        {
            for (int i = 0; i < semesters.Length; i++)
            {
                semesters[i] = new semester();
            }
        }

    }
}
