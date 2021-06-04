using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    class seminarSubject : subject
    {
        protected override int? getAverageGradeFromExams()
        {
            throw new NotImplementedException();
        }
        public seminarSubject(bool isW) : base(isW ? subjectTypes.WS : subjectTypes.PS) { }
    }
}
