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
        protected override int? getAverageGradeFromExams()
        {
            if (grade != null) return 4 * grade;
            else return null;
        }
    }
}