using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbiCALC.selections;

namespace AbiCALC
{
    [Serializable]
    public class mins
    {
        public Dictionary<subjectTypes, int> m = new Dictionary<subjectTypes, int>();
        public Dictionary<subjectTypes, int> belegt = new Dictionary<subjectTypes, int>();
        private List<abiexam> abis = new List<abiexam>();
        public subjectTypes special1, special2;
        public int specialMin = 1, specialMax = 3;

        public mins(selection s)
        {
            abis = s.abis;
            setDefaults();
            sel(s);
            setSpecial(s);
            updateBelegt(s);
        }

        private void updateBelegt(selection s)
        {
            foreach (subjectTypes subject in m.Keys)
            {
                int x = 4;

                if (subject.t == subjectTypes.type.WSem || subject.t == subjectTypes.type.PSem) x = 3;
                if (subject.t == subjectTypes.type.Profil) x = s.dp.profil[subject.name] ? 4 : 2;
                if (subject.t == subjectTypes.type.GeoWirtschaft) x = (bool)s.GeoWirtschaft12 ? 4 : 2;

                if (s.ssic.isDefined)
                    if (!s.ssic.data1.continueExtra12)
                        if (s.ssic.data1.extra == subject) x = 2;

                belegt[subject] = x;
            }
        }

        private void sel(selection s)
        {
            //set zweifachauswahl
            subjectTypes.Reli.name = s.dp.rt.ToString();
            subjectTypes.KunstOderMusik.name = s.dp.km.ToString();
            subjectTypes.GeoOderWirtschaft.name = s.dp.gw.ToString();
            //set name
            subjectTypes.WS.name = s.dp.WSemName;
            subjectTypes.PS.name = s.dp.PSemName;
            //profil
            foreach (string st in s.dp.profil.Keys)
            {
                m.Add(new subjectTypes(subjectTypes.type.Profil, st), 0);
            }


            //Geschichte und Sozi => Geo oder Wirtschaft
            foreach (subjectTypes x in s.gesSoz)
            {
                m[x] = 3;
            }
            if ((bool)s.SoziGeschichteZsm)
            {
                m.Add(subjectTypes.GeoOderWirtschaft, 3);
            }
            else
            {
                m.Add(subjectTypes.GeoOderWirtschaft, 1);
            }
        }


        private void setDefaults()
        {
            m[subjectTypes.Deutsch] = 4;
            m[subjectTypes.Mathe] = 4;

            m[subjectTypes.Sport] = 0;

            m[subjectTypes.Reli] = 3;
            m[subjectTypes.KunstOderMusik] = 3;
        }

        private void setSpecial(selection s) 
        {
            if(s.ssic.isDefined) 
            {
                specialSubjectInfoContainer.sepecialSubjectInfoDefinedOrder x = s.ssic.data1;
                m[x.language1] = 4;
                m[x.science1] = x.extra.t == subjectTypes.type.Naturwissenschaft ? 3 : 4;
                m[x.extra] = 1;
            }
            else 
            {
                specialSubjectInfoContainer.specialSubjectInfoUndefinedOrder x = s.ssic.data2;
                m[x.defined] = 4;
                special1 = x.notDefined1;
                special2 = x.notDefined2;
            }
        }

        public int this[subjectTypes key]
        {
            get => GetValue(key);
        }

        private int GetValue(subjectTypes key)
        {
            bool b = false;
            foreach (abiexam a in abis)
                if (a.type == key)
                {
                    b = true;
                    break;
                }
            return b ? 4 : m[key];
        }
    }
}
