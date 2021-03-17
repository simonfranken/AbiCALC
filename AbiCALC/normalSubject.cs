using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    class normalSubject : subject
    {
        List<exam> exams = new List<exam>();

        public override int? getAverageGrade()
        {
            int smallSumm = 0, smallCount = 0, bigSumm = 0, bigCount = 0;
            foreach (exam e in exams)
            {
                if(e.isBig) 
                {
                    bigSumm += e.grade;
                    bigCount++;
                }
                else 
                {
                    smallSumm += e.grade;
                    smallCount++;
                }
            }
            float smallAvg = smallCount != 0 ? smallSumm / smallCount : float.NaN;
            float bigAvg = bigCount != 0 ? bigSumm / bigCount : float.NaN;
            float r;
            if(smallCount != float.NaN && bigAvg != float.NaN) 
            {
                r = (smallAvg + bigAvg) / 2f;
            }
            else if(bigAvg != float.NaN && smallAvg == float.NaN) 
            {
                r = bigAvg;
            }
            else if(bigAvg == float.NaN && smallAvg != float.NaN) 
            {
                r = smallAvg;
            }
            else 
            {
                r = float.NaN;
            }
            return r != float.NaN ? (int)r : null;
        }
    }
}
