using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class abiexam : subject
    {
        int? grade = null;
        public abiexam(subjectTypes s) : base(s, false) { }

        internal int? getGrade()
        {
            if (grade != null) return 4 * grade;
            else return null;
        }
    }
}