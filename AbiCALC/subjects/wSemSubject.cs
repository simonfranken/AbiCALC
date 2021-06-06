using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.subjects
{
    [Serializable]
    public class wSemSubject : subject
    {
        public wSemSubject() : base(subjectTypes.WS) { }
        static List<exam> small = new List<exam>();
        static exam semArbeit = null;
        public static List<int> getGrades()
        {
            int x = getSmallAvg();
            int y = getBig();
            return new List<int> {x,x,y,y};
        }

        private static int getSmallAvg() 
        {
            fraction i = (fraction)0;
            int s = 0;
            foreach (var item in small)
            {
                i += item.weight;
                s += item.grade;
            }
            fraction r = ((fraction)s) / i;
            return r.isDivZeroError() ? 8 : (int)r;
        }
        private static int getBig() 
        {
            return semArbeit != null ? semArbeit.grade : getSmallAvg();
        }
    }
}
