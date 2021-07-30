using System;
using System.Collections.Generic;
using lib.interfaces;
using System.Windows.Media;
using AbiCALC.serialization;

namespace AbiCALC
{
    [Serializable]
    public class subjectTypes : IName
    {

        public subjectTypes(type _t) : this(_t, _t.ToString()) { }
        public subjectTypes(type _t, string s) : this(_t, baseSubjetTypes.D, s) { }
        private subjectTypes(type _t, baseSubjetTypes _ct, string s)
        {
            t = _t;
            ct = _ct;
            name.itemValue = s;
            odc = overrideDictContainer.singleton;
        }


        public color2 c = new Color { R = 127, A = 255, G = 127 };
        private observableItem<string> name = new observableItem<string>();
        public type t;
        public baseSubjetTypes ct;
        public List<subject> instances = new List<subject>();
        public abiexam a = null;
        private overrideDictContainer odc;

        public enum type
        {
            Naturwissenschaft, Fremdsprache, Deutsch, Mathe, KunstMusik, GeoWirtschaft, Profil, WSem, PSem, Reli, Sport, Geschichte, Sozi, Informatik
        }
        public static readonly subjectTypes Deutsch = new subjectTypes(type.Deutsch);
        public static readonly subjectTypes Mathe = new subjectTypes(type.Mathe);
        public static readonly subjectTypes Sport = new subjectTypes(type.Sport);
        public static readonly subjectTypes Reli = new subjectTypes(type.Reli);
        public static readonly subjectTypes KunstOderMusik = new subjectTypes(type.KunstMusik);
        public static readonly subjectTypes GeoOderWirtschaft = new subjectTypes(type.GeoWirtschaft);
        public static readonly subjectTypes WS = new subjectTypes(type.WSem, baseSubjetTypes.W, "W-Seminar");
        public static readonly subjectTypes PS = new subjectTypes(type.PSem, baseSubjetTypes.P, "P-Seminar");

        public observableItem<string> Name => name;

        public override string ToString()
        {
            return name.itemValue;
        }

        private subjectTypes conneted = null;
        public void setConnected(subjectTypes value, int a, int b)
        {
            conneted = value;
            value.conneted = this;
            odc.overrideDict[getOverrideId()] = (this, value, a, b);
        }

        public bool overrideGrades() 
        {
            return conneted != null;
        }

        public string getOverrideId() 
        {
            List<string> l = new List<string>();
            l.Add(this.Name.itemValue);
            l.Add(this.conneted.Name.itemValue);
            l.Sort();
            string ret =  string.Format("{0} & {1}", l[0], l[1]);
            return ret;
        }

        public static List<int> getOverrideValues(string s) 
        {
            (subjectTypes a, subjectTypes b, int iA, int iB) x = overrideDictContainer.singleton.overrideDict[s];
            List<int> lA = getGrades(x.a);
            List<int> lB = getGrades(x.b);
            bool b = lA.Count > lB.Count;
            int y = b ? lB.Count : lA.Count;
            int l = !b ? lB.Count : lA.Count;
            List<int> ret = new List<int>();
            int i = 0;
            for (i = 0; i < y; i++)
            {
                ret.Add((lA[i] * x.iA + lB[i] * x.iB)/(x.iA + x.iB));
            }
            for (; i < l; i++)
            {
                ret.Add(b ? lA[i] : lB[i]);
            }
            return ret;
        }

        public static List<int> getGrades(subjectTypes st)
        {
            switch (st.ct.t)
            {
                case "W":
                    return subjects.wSemSubject.getGrades();
                case "P":
                    return subjects.pSemSubject.getGrades();
                case "D":
                    List<int> ret = new List<int>();
                    int i = 0;
                    int s = 0;
                    Dictionary<normalSubject, int?> dict = new Dictionary<normalSubject, int?>();
                    foreach (normalSubject item in st.instances)
                    {
                        dict[item] = item.getGrade();
                    }
                    foreach (normalSubject item in dict.Keys)
                    {
                        if (dict[item] != null)
                        {
                            i++;
                            s += (int)dict[item];
                            ret.Add((int)dict[item]);
                        }
                    }
                    int avg = i > 0 ? s / i : 8;
                    foreach (normalSubject item in dict.Keys)
                    {
                        if (dict[item] == null)
                        {
                            ret.Add(avg);
                        }
                    }
                    if (st.a != null)
                    {
                        int? x = st.a.getGrade();
                        if (x != null)
                        {
                            ret.Add((int)x);
                        }
                        else
                        {
                            int temp = 0;
                            foreach (int f in ret)
                            {
                                temp += f;
                            }
                            avg = temp / ret.Count;
                            ret.Add(avg * 4);
                        }
                    }
                    return ret;
                default:
                    throw new Exception("Type not found");

            }
        }
    }
}