using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiCALC.selections
{
    public class selection
    {

        public selection(preSelection _ps) 
        {
            ps = _ps;
        }

        public List<abiexam> abis 
        {
            get => ssic.abis;
        }

        public string name = "";
        public preSelection ps;
        public defaultProperties dp = new defaultProperties();

        public string FremdsprachenIndex = "";
        public int NaturWissenschaftsIndex = -1;
        public string Index2;

        public List<subjectTypes> gesSoz = new List<subjectTypes>();

        public bool? SoziGeschichteZsm;

        public bool? GeoWirtschaft12 = null;
        public bool? Extra12 = null;

        public specialSubjectInfoContainer ssic;

        public bool? ges2soz1 = null;

        public List<List<subjectTypes>> getAbiPos() 
        {
            string s = "";
            if (!isValid(ref s)) throw new Exception();
            List<List<subjectTypes>> r = new List<List<subjectTypes>>();

            r.Add(new List<subjectTypes> { subjectTypes.Deutsch });
            r.Add(new List<subjectTypes> { subjectTypes.Mathe });
            List<subjectTypes> lan = new List<subjectTypes>();
            List<subjectTypes> ges = new List<subjectTypes>();
            List<subjectTypes> rest = new List<subjectTypes>();
            r.Add(lan);
            r.Add(ges);
            r.Add(rest);

            if(ssic.isDefined) 
            {
                lan.Add(ssic.data1.language1);
            }
            else 
            {
                if(ssic.data2.defined.t == subjectTypes.type.Fremdsprache) 
                {
                    lan.Add(ssic.data2.defined);
                }
                else 
                {
                    lan.Add(ssic.data2.notDefined1);
                    lan.Add(ssic.data2.notDefined2);
                }
            }

            ges.Add(subjectTypes.Reli);
            if((bool)GeoWirtschaft12) ges.Add(subjectTypes.GeoOderWirtschaft);
            foreach (var x in gesSoz)
            {
                ges.Add(x);
            }
            rest.Add(subjectTypes.Sport);
            rest.Add(subjectTypes.KunstOderMusik);
            if(ssic.isDefined) 
            {
                rest.Add(ssic.data1.language1);
                rest.Add(ssic.data1.science1);
                if (ssic.data1.continueExtra12) rest.Add(ssic.data1.extra);
            }
            else 
            {
                rest.Add(ssic.data2.defined);
                rest.Add(ssic.data2.notDefined1);
                rest.Add(ssic.data2.notDefined2);
            }

            return r;
        }
        public bool abisValid(List<subjectTypes> abis, ref string s) 
        {
            if (abis.Distinct().Count() != abis.Count) 
            {
                s = "Du kannst in einem Fach nur ein Mal Abitur schreiben.";
                return false;
            }
            if(abis.Contains(null)) 
            {
                s = "Du musst alle Felder auswählen!";
                return false;
            }
            return true;
        }
        public Dictionary<string, subjectTypes.type> getExtras() 
        {
            Dictionary<string, subjectTypes.type> ret = new Dictionary<string, subjectTypes.type>();

            if(ps.forceExtraLanguage) 
            {
                foreach (var x in ps.lansSecondary) ret.Add(x, subjectTypes.type.Fremdsprache);
            }
            else 
            {
                foreach (string x in ps.lansSecondary)
                {
                    ret.Add(x, subjectTypes.type.Fremdsprache);
                }
                if(ps.g == preSelection.GymType.NTG) 
                {
                    ret.Add("Informatik", subjectTypes.type.Informatik);
                }
                foreach (ScienceType x in (ScienceType[])Enum.GetValues(typeof(ScienceType)))
                {
                    ret.Add(x.ToString(), subjectTypes.type.Naturwissenschaft);
                }
            }

            return ret;
        }

        public bool isValid(ref string error) 
        {
            if(NaturWissenschaftsIndex == -1) 
            {
                error = "Die Naturwissenschaft darf nicht leer sein.";
                return false;
            }
            if (string.IsNullOrEmpty(FremdsprachenIndex))
            {
                error = "Die Fremdsprache darf nicht leer sein.";
                return false;
            }
            if (GeoWirtschaft12 == null) 
            {
                error = "Geo/Wirtschaft in Q12 darf nicht leer sein.";
                return false;
            }
            if (Extra12 == null)
            {
                error = "Extra-Fach in Q12 darf nicht leer sein.";
                return false;
            }
            if(SoziGeschichteZsm == null) 
            {
                error = "Sozialkunde und Geschichte zusammen? darf nicht leer sein.";
                return false;
            }
            if (Index2 == ((ScienceType)NaturWissenschaftsIndex).ToString() || Index2 == FremdsprachenIndex) 
            {
                error = "Du kannst keine 2 Identischen Fächer wählen.";
                return false;
            }
            if(ps.g != preSelection.GymType.WSG && !(bool)SoziGeschichteZsm) 
            {
                error = "Geschichte und Sozialkunde zusammen ist nur möglich mit WSG.";
                return false;
            }
            if((bool)SoziGeschichteZsm && !(bool)GeoWirtschaft12) 
            {
                error = "Geo/Wirtischaft nicht in Q12 ist nur möglich mit Sozialkunde und Geschichte zusammen";
                return false;
            }
            if (!(bool)Extra12 && ps.forceExtraLanguage)
            {
                error = "Die spätbeginnende Fremdsprache muss in Q12 belegt werden.";
                return false;
            }
            if(ges2soz1 == null) 
            {
                if((bool)SoziGeschichteZsm) 
                {
                    error = "Entweder Geschichte oder Sozialkunde muss 2 stündig belegt werden.";
                    return false;
                }
            }
            else
            {
                if(!(bool)SoziGeschichteZsm) 
                {
                    error = "Wenn Geschichte und Sozialkunde zusammen sind, sind beide automatisch 2 stündig.";
                    return false;
                }
                if(!(bool)ges2soz1 && !(ps.g == preSelection.GymType.WSG )) 
                {
                    error = "Sozialkunde kann nur in WSG 2 stündig belegt werden.";
                    return false;
                }
            }
            if (!dp.isValid(ref error)) 
            {
                return false;
            }
            return true;
        }

        public void setSSIC() 
        {
            ssic = new specialSubjectInfoContainer(this);
            //set zweifachauswahl
            subjectTypes.Reli.Name.itemValue = dp.rt.ToString();
            subjectTypes.KunstOderMusik.Name.itemValue = dp.km.ToString();
            subjectTypes.GeoOderWirtschaft.Name.itemValue = dp.gw.ToString();
            //set name
            subjectTypes.WS.Name.itemValue = dp.WSemName;
            subjectTypes.PS.Name.itemValue = dp.PSemName;
            test();
        }

        private void test() 
        {
            if ((bool)SoziGeschichteZsm)
            {
                var x = new subjectTypes(subjectTypes.type.Geschichte);
                var y = new subjectTypes(subjectTypes.type.Sozi);
                gesSoz.Add(x);
                gesSoz.Add(y);
                x.setConnected(y, (bool)ges2soz1 ? 2 : 1, (bool)ges2soz1 ? 1 : 2);
            }
            else
            {
                gesSoz.Add(new subjectTypes(subjectTypes.type.Geschichte));
                gesSoz.Add(new subjectTypes(subjectTypes.type.Sozi));
            }
        }

        public enum ReliType 
        {
            Ethik, Evangelisch, Katholisch
        }
        public enum KunstOrMusikType 
        {
            Musik, Kunst
        }
        public enum GeoOrWrType 
        {
            Geo, Wirtschaft
        }
        public enum ScienceType 
        {
            Physik, Biologie, Chemie  
        }
        
    }
}