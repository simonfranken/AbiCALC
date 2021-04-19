using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public class exam
    {
        public bool isBig;
        public examType? type;
        public int grade;
        public fraction weight = new fraction(1);

        public exam(bool b, int i) 
        {
            isBig = b;
            grade = i;
        }
    }
}
