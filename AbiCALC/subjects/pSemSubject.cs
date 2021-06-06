using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.subjects
{
    [Serializable]
    public class pSemSubject : subject
    {
        public pSemSubject() : base(subjectTypes.PS) { }
        public static List<exam> exams = new List<exam>();
        public static List<int> getGrades()
        {
            int x = getAvg();
            return new List<int> { x, x };
        }

        private static int getAvg()
        {
            fraction i = (fraction)0;
            int s = 0;
            foreach (var item in exams)
            {
                i += item.weight;
                s += item.grade;
            }
            fraction r = ((fraction)s) / i;
            return r.isDivZeroError() ? 8 : (int)r;
        }
    }
}
