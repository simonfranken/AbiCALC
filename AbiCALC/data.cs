using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable()]
    public class data : IName
    {
        private semester[] semesters = new semester[4];
        private List<abiexam> abiexams = new List<abiexam>();
        private observableItem<string> _name = new observableItem<string>();
        private mins min;

        [OnDeserialized]
        private void deserialized(StreamingContext context)
        {
            _name.func = (string s) => { return $"Hallo, {s}!"; };
            _name.PropertyChanged += (object? sender, PropertyChangedEventArgs e) => { serialization.database.saveCurrent(); };
        }

        internal IEnumerable<subjectTypes> getSubjectTypes()
        {
            List<subjectTypes> ret = new List<subjectTypes>();
            foreach (semester item in semesters)
            {
                foreach (subjectTypes item2 in item.dict.Keys)
                {
                    if (!ret.Contains(item2)) ret.Add(item2);
                }
            }
            return ret;
        }

        public int getPoints() 
        {
            return getMaxPoints(predict(new List<semester>(semesters)), min, predict(new List<abiexam>(abiexams), new List<semester>(semesters)));
        }

        public observableItem<string> Name 
        {
            get => _name;
        }
        public data(selections.selection _selection)
        {
            deserialized(default);
            abiexams = _selection.abis;
            min = new mins(_selection);
            for (int i = 0; i < semesters.Length; i++)
            {
                semesters[i] = new semester();
            }
            foreach (subjectTypes item in min.belegt.Keys)
            {
                for (int i = 0; i < min.belegt[item] - 1; i++)
                {
                    semesters[i].dict[item] = subject.constructor(item);
                }
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
            //init vars
            int count = 0;
            int pointsSumm = 0;
            all = new List<subject>(all.Distinct());
            all.OrderBy(x => (int)x.getAverageGrade());
            all.Reverse();

            //summ up min per subject
            foreach(subjectTypes st in d.Keys) 
            {
                pointsSumm += summ(max(d[st], m[st], used));
                count++;
            }
            //remove used
            foreach(subject s in used) 
            {
                all.Remove(s);
            }
            //add rest
            pointsSumm += summ(max(all, (required - count), new List<subject>()));
            //add abi
            foreach(abiexam a in abi) 
            {
                pointsSumm += (int)a.getAverageGrade();
            }
            return pointsSumm;
        }
        //gets the highst i items in l, adds them to used
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
        //summs all grades up
        private static int summ(List<subject> l) 
        {
            int i = 0;
            foreach (subject x in l) i += (int)x.getAverageGrade();
            return i;
        }
        //predict empty subjects by avg 1
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
        //predict empty subjects by avg 2
        public static List<subject> predict(List<subject> subjects) 
        {
            fraction summ = new fraction(0), count = new fraction(0);
            Dictionary<bool, List<subject>> d = new Dictionary<bool, List<subject>>();
            List<subject> ret = new List<subject>();
            //split in 2 lists
            foreach(subject s in subjects) 
            {
                (d[s.examsValid()] ??= new List<subject>()).Add(s);
            }
            //calc avg based on "true"-list
            foreach(subject s in d[true]) 
            {
                s.noOverride();
                count++;
                summ += (fraction)s.getAverageGrade();
                ret.Add(s);
            }
            int avg = summ == 0 ? (15 / 2) * subject.getPredictionFactor(subjects[0]) : (int)(((fraction)(summ)) / ((fraction)(count)));
            //set override of "false"-list to avg
            foreach(subject s in d[false]) 
            {
                s.overridePoints = avg;
                ret.Add(s);
            }
            return ret;
        }
        //predict abi by avg 1
        public static List<abiexam> predict(List<abiexam> abis, List<semester> sems) 
        {
            List<abiexam> ret = new List<abiexam>();
            foreach(abiexam a in abis) 
            {
                ret.Add(predict(a, sems));
            }
            return ret;
        }
        //predict abi by avg 2
        public static abiexam predict(abiexam abi, List<semester> sems) 
        {
            if (!abi.examsValid())
            {
                subjectTypes t = abi.type;
                sems = predict(sems);
                int count = 0;
                int summ = 0;
                foreach (semester s in sems)
                    foreach (subject su in s.dict.Values)
                        if (su.type == t)
                        {
                            count++;
                            summ += (int)su.getAverageGrade();
                        }
                abi.overridePoints = count != 0 ? (summ / count) : 15 / 2 * subject.getPredictionFactor(abi);
            }
            else abi.noOverride();
            return abi;
        }
    }
}
