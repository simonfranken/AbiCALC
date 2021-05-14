using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public class normalSubject : subject
    {
        List<exam> exams = new List<exam>();

        public void add(exam e) => exams.Add(e);

        public override bool isValid() 
        {
            int i = 0;
            foreach (exam e in exams)
            {
                if (e.isBig) i++;
            }
            return i <= 1;
        }

        protected override int? getAverageGradeFromExams()
        {
            int smallSumm = 0;
            fraction smallCount = (fraction)(0);
            foreach (exam e in exams)
            {
                if (!e.isBig)
                {
                    smallSumm += e.grade;
                    smallCount += e.weight;
                }
            }
            fraction smallAvg = (fraction)smallSumm / smallCount;
            exam b = getBig();
            fraction bigAvg = b != null ? (fraction)b.grade : null;
            fraction r;

            if(!smallAvg.isDivZeroError() && bigAvg != null) 
            {
                r = (smallAvg.round2Decimals() + bigAvg) / (fraction)2;
            }
            else if(!smallAvg.isDivZeroError() && bigAvg == null) 
            {
                r = smallAvg.round2Decimals();
            }
            else if(smallAvg.isDivZeroError() && bigAvg != null) 
            {
                r = bigAvg;
            }
            else 
            {
                r = null;
            }
            return r.round2Decimals().rounded();
        }

        private exam getBig()
        {
            foreach (exam e in exams)
            {
                if (e.isBig) return e;
            }
            return null;
        }
    }
}
