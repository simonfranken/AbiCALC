using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    class seminarSubject : subject
    {
        protected override int? getAverageGradeFromExams()
        {
            throw new NotImplementedException();
        }

        public override bool isValid()
        {
            throw new NotImplementedException();
        }
    }
}
