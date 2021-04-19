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

        public override int? getAverageGrade()
        {
            int smallSumm = 0, bigSumm = 0;
            fraction smallCount = (fraction)(0), bigCount = (fraction)(0);
            foreach (exam e in exams)
            {
                if(e.isBig) 
                {
                    bigSumm += e.grade;
                    bigCount += e.weight;
                }
                else 
                {
                    smallSumm += e.grade;
                    smallCount += e.weight;
                }
            }
            fraction smallAvg = (fraction)smallSumm / smallCount;
            fraction bigAvg = (fraction)bigSumm / bigCount;
            fraction r;
            if(!smallAvg.isDivZeroError() && !bigAvg.isDivZeroError()) 
            {
                r = (smallAvg + bigAvg) / (fraction)2;
            }
            else if(!smallAvg.isDivZeroError() && bigAvg.isDivZeroError()) 
            {
                r = smallAvg;
            }
            else if(smallAvg.isDivZeroError() && !bigAvg.isDivZeroError()) 
            {
                r = bigAvg;
            }
            else 
            {
                r = null;
            }
            return r;
        }
    }
}
