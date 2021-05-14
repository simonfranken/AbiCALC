using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable()]
    class data
    {
        semester[] semesters = new semester[4];
        abiexam[] abiexams = new abiexam[5];
        private string _name = string.Empty;
        private mins min = new mins();


        public int getPoints() 
        {
            return getMaxPoints(predict(new List<semester>(semesters)), min, new List<abiexam>(abiexams));
        }

        public string name 
        {
            get => _name;
        }

        public data()
        {
            for (int i = 0; i < semesters.Length; i++)
            {
                semesters[i] = new semester();
            }
            for (int i = 0; i < abiexams.Length; i++)
            {
                abiexams[i] = new abiexam();
            }
        }

        public static int getMaxPoints(List<semester> sems, mins m, List<abiexam> abi) 
        {
            Dictionary<subjectTypes, List<subject>> d = new Dictionary<subjectTypes, List<subject>>();
            List<subject> used = new List<subject>();
            List<subject> all = new List<subject>();
            int required = 40;
            //reorganize by subject
            foreach (semester s in sems)
            {
                foreach (subject su in s.dict.Values)
                {
                    (d[su.type] ??= new List<subject>()).Add(su);
                    all.Add(su);
                }
            }
            int count = 0;
            int pointsSumm = 0;
            all.OrderBy(x => (int)x.getAverageGrade());
            all.Reverse();
            foreach(subjectTypes st in d.Keys) 
            {
                pointsSumm += summ(max(d[st], m.m[st], used));
                count++;
            }
            foreach(subject s in used) 
            {
                all.Remove(s);
            }
            pointsSumm += summ(max(all, (required - count), new List<subject>()));
            foreach(abiexam a in abi) 
            {
                pointsSumm += a.grade;
            }
            return pointsSumm;
        }

        private static List<subject> max(List<subject> l, int i, List<subject> used) 
        {
            l.OrderBy(x => (int)x.getAverageGrade());
            l.Reverse();
            List<subject> ret = new List<subject>();
            for (int x = 0; x < i; x++) 
            {
                ret.Add(l[x]);
                used.Add(l[x]);
            }
            return ret;
        }

        private static int summ(List<subject> l) 
        {
            int i = 0;
            foreach (subject x in l) i += (int)x.getAverageGrade();
            return i;
        }

        public static List<semester> predict(List<semester> origin) 
        {
            Dictionary<subjectTypes, List<subject>> d = new Dictionary<subjectTypes, List<subject>>();
            Dictionary<subjectTypes, List<subject>> d2 = new Dictionary<subjectTypes, List<subject>>();
            //reorganize by subject
            foreach(semester s in origin) 
            {
                foreach(subject su in s.dict.Values) 
                {
                    (d[su.type] ??= new List<subject>()).Add(su);
                }
            }
            //predict each subject
            foreach(subjectTypes st in d.Keys) 
            {
                d2[st] = predict(d[st]);
            }
            //reorganize by semester
            List<semester> ret = new List<semester>();
            foreach(subjectTypes st in d2.Keys) 
            {
                for(int i = 0; i < d2[st].Count; i++) 
                {
                    if (ret.Count <= i) ret.Add(new semester());
                    ret[i].dict[st] = d2[st][i];
                }
            }
            return ret;
        }

        public static List<subject> predict(List<subject> subjects) 
        {
            fraction summ = new fraction(0), count = new fraction(0);
            Dictionary<bool, List<subject>> d = new Dictionary<bool, List<subject>>();
            List<subject> ret = new List<subject>();
            foreach(subject s in subjects) 
            {
                (d[s.examsValid()] ??= new List<subject>()).Add(s);
            }
            foreach(subject s in d[true]) 
            {
                count++;
                summ += (fraction)s.getAverageGrade();
                ret.Add(s);
            }
            int avg = summ == 0 ? 15 / 2 : (int)(((fraction)(summ)) / ((fraction)(count)));
            foreach(subject s in d[false]) 
            {
                s.overridePoints = avg;
                ret.Add(s);
            }
            return ret;
        }
    }
}
