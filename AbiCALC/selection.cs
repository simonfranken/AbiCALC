using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiCALC
{
    public class selection
    {

        public preSelection ps;
        public defaultProperties dp = new defaultProperties();

        public string FremdsprachenIndex;
        public int NaturWissenschaftsIndex;

        public List<subjectTypes> gesSoz = new List<subjectTypes>();

        public string Index2;

        public bool abisValid(List<subjectTypes> abis) 
        {
            return abis.Distinct().Count() == abis.Count;
        }

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
            ges.Add(subjectTypes.GeoOderWirtschaft);
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

        public bool SoziGeschichteZsm 
        {
            get => _SoziGeschichteZsm;
            set 
            {
                _SoziGeschichteZsm = value;
                gesSoz.Clear();
                if(SoziGeschichteZsm) 
                {
                    gesSoz.Add(new subjectTypes(subjectTypes.type.GeschichteSozi));
                }
                else 
                {
                    gesSoz.Add(new subjectTypes(subjectTypes.type.Geschichte));
                    gesSoz.Add(new subjectTypes(subjectTypes.type.Sozi));
                }
            }
        }
        public bool _SoziGeschichteZsm;

        public specialSubjectInfoContainer ssic;

        private bool? _GeoWirtschaft12 = null;
        private bool? _Extra12 = null;

        public bool gw12 
        {
            get => (_GeoWirtschaft12 != null) ? (bool)_GeoWirtschaft12 : true;
            set 
            {
                if (requireGW12()) _GeoWirtschaft12 = value;
                else throw new Exception("Not allowed to set Property");
            }
        }

        public bool e12
        {
            get => (_Extra12 != null) ? (bool)_Extra12 : true;
            set
            {
                if (requireExtra12()) _Extra12 = value;
                else throw new Exception("Not allowed to set Property");
            }
        }

        public bool requireGW12() 
        {
            return !SoziGeschichteZsm;
        }

        public bool requireExtra12() 
        {
            return !ps.forceExtraLanguage;
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
            if(Index2 == ((ScienceType)NaturWissenschaftsIndex).ToString() || Index2 == FremdsprachenIndex) 
            {
                error = "Du kannst keine 2 Identischen Fächer wählen.";
                return false;
            }
            if(!dp.isValid(ref error)) 
            {
                return false;
            }
            return true;
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
        
        public class specialSubjectInfoContainer 
        {
            public sepecialSubjectInfoDefinedOrder data1 = null;
            public specialSubjectInfoUndefinedOrder data2 = null;
            public bool isDefined 
            {
                get 
                {
                    if (data1 != null ^ data2 != null) 
                    {
                        return data1 != null;
                    }
                    else 
                    {
                        throw new Exception("data1 and data2 can not be set both or neither.");
                    }
                }
            }
            

            public void update(List<abiexam> a) 
            {
                if(!isDefined) 
                {
                    bool b = false;
                    subjectTypes s = null, s2 = null;
                    foreach(abiexam abi in a) 
                    {
                        if(abi.type == data2.notDefined1 || abi.type == data2.notDefined2) 
                        {
                            b = true;
                            s = abi.type == data2.notDefined1 ? data2.notDefined1 : data2.notDefined2;
                            s2 = abi.type != data2.notDefined1 ? data2.notDefined1 : data2.notDefined2;
                            break;
                        }
                    }
                    if(b) 
                    {
                        data1 = new sepecialSubjectInfoDefinedOrder();
                        data1.continueExtra12 = true;
                        data1.extra = s2;
                        if (data2.defined.t == subjectTypes.type.Naturwissenschaft) 
                        {
                            data1.science1 = data2.defined;
                            data1.language1 = s;
                        }
                        else 
                        {
                            data1.language1 = data2.defined;
                            data1.science1 = s;
                        }
                        data2 = null;
                    }
                }
            }

            public specialSubjectInfoContainer(selection parent) 
            {
                subjectTypes.type t = parent.getExtras()[parent.Index2];
                if(t == subjectTypes.type.Informatik) 
                {
                    data1 = new sepecialSubjectInfoDefinedOrder();
                    data1.science1 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                    data1.language1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                    data1.extra = new subjectTypes(subjectTypes.type.Informatik);
                    data1.continueExtra12 = parent.e12;
                }
                else 
                {
                    if(parent.ps.forceExtraLanguage)
                    {
                        data1 = new sepecialSubjectInfoDefinedOrder();
                        data1.science1 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                        data1.language1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                        data1.extra = new subjectTypes(subjectTypes.type.Fremdsprache, new List<string>(parent.getExtras().Keys)[0]);
                        data1.continueExtra12 = parent.e12;
                    }
                    else 
                    {
                        if(!parent.e12) 
                        {
                            data1 = new sepecialSubjectInfoDefinedOrder();
                            data1.language1 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                            data1.language1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                            data1.extra = new subjectTypes(parent.getExtras()[parent.Index2], parent.Index2);
                            data1.continueExtra12 = parent.e12;
                        }
                        else 
                        {
                            if(parent.getExtras()[parent.Index2] == subjectTypes.type.Fremdsprache) 
                            {
                                data2 = new specialSubjectInfoUndefinedOrder();
                                data2.defined = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                                data2.notDefined1 = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                                data2.notDefined2 = new subjectTypes(parent.getExtras()[parent.Index2], parent.Index2);
                            }
                            else 
                            {
                                data2 = new specialSubjectInfoUndefinedOrder();
                                data2.defined = new subjectTypes(subjectTypes.type.Fremdsprache, parent.FremdsprachenIndex);
                                data2.notDefined2 = new subjectTypes(subjectTypes.type.Naturwissenschaft, ((ScienceType)(parent.NaturWissenschaftsIndex)).ToString());
                                data2.notDefined2 = new subjectTypes(parent.getExtras()[parent.Index2], parent.Index2);
                            }
                        }
                    }
                }
            }
            
            public class sepecialSubjectInfoDefinedOrder 
            {
                public subjectTypes science1, language1, extra;
                public bool continueExtra12;
            }
            public class specialSubjectInfoUndefinedOrder 
            {
                public subjectTypes notDefined1, notDefined2, defined;
            }
        }
    
        public class defaultProperties 
        {
            //zweifachauswahl
            public ReliType? rt = null;
            public KunstOrMusikType? km = null;
            public GeoOrWrType? gw = null;


            //namen
            public string WSemName = null, PSemName = null;

            //profil
            public Dictionary<string, bool> profil = null;

            public bool isValid(ref string error) 
            {
                if(string.IsNullOrEmpty(WSemName) || string.IsNullOrEmpty(PSemName))
                {
                    error = "Der Seminar-Name darf nicht leer sein.";
                    return false;
                }

                if (profil == null)
                {
                    return false;
                }
                if (rt == null) 
                {
                    error = "Bitte wähle eine Religion aus.";
                    return false;
                }
                if(km == null) 
                {
                    error = "Biite wähle entweder Kunst oder Musik.";
                    return false;
                }
                if(gw == null) 
                {
                    error = "Biite wähle entweder Geo oder Wirtschaft aus.";
                    return false;
                }

                return true;
            }
        }
    }
}