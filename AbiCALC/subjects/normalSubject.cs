using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class normalSubject : subject
    {
        public normalSubject(subjectTypes s) : base(s) { }
        
        List<exam> examsSmall = new List<exam>();
        exam klausur = null;

        //public void add(exam e) => examsSmall.Add(e);

        public int? getGrade()
        {
            //init
            int smallSumm = 0;
            fraction smallCount = (fraction)(0);
            //summ up
            foreach (exam e in examsSmall)
            {
                    smallSumm += e.grade;
                    smallCount += e.weight;               
            }
            //calc big
            fraction smallAvg = (fraction)smallSumm / smallCount;
            fraction bigAvg = klausur != null ? (fraction)klausur.grade : null;
            fraction r;
            switch((!smallAvg.isDivZeroError(), bigAvg != null)) 
            {
                //both
                case (true, true):
                    r = (smallAvg.round2Decimals() + bigAvg) / (fraction)2;
                    break;
                //none
                case (false, false):
                    r = null;
                    break;
                //only big
                case (false, true):
                    r = bigAvg;
                    break;
                //only small
                case (true, false):
                    r = smallAvg.round2Decimals();
                    break;
            }
            return r != null ? r.round2Decimals().rounded() : null;
        }
    }
}
