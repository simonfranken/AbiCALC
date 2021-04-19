using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public abstract class subject
    {
        subjectTypes type;
        public abstract int? getAverageGrade();
    }
}
