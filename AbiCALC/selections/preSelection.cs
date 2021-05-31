using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public class preSelection
    {
        public enum GymType 
        {
            SG, NTG, NUG, WSG
        }

        public GymType? g = null;
        public string lan1 = null, lan2 = null, lan3 = null, lanLate = null, lanW = null;
        public bool? useLanLate = null;
        public bool? replaceLan1 = null;

        public bool forceExtraLanguage 
        {
            get 
            {
                if (!isValid()) return false;
                return (bool)useLanLate;
            }
        }

        public List<string> lansPrimary 
        {
            get 
            {
                if (!isValid()) return new List<string>();
                List<string> r = new List<string>();

                r.Add(lan1);
                r.Add(lan2);
                if (g == GymType.SG) r.Add(lan3);
                
                return r;
            }
        }
        public List<string> lansSecondary 
        {
            get 
            {
                if (!isValid()) return new List<string>();
                List<string> r = new List<string>();

                if((bool)useLanLate) 
                {
                    r.Add(lanLate);
                }
                else 
                {
                    r.Add(lan1);
                    r.Add(lan2);
                    if (g == GymType.SG) r.Add(lan3);
                    if (!string.IsNullOrEmpty(lanW)) r.Add(lanW);
                }

                return r;
            }
        }

        private bool isValid() 
        {
            string s = "";
            return isValid(ref s);
        }

        public bool isValid(ref string error) 
        {
            if(g == null) 
            {
                error = "Der Gymnasiumtyp darf nicht leer sein.";
                return false;
            }
            if (string.IsNullOrEmpty(lan1)) 
            {
                error = "Die erste Fremdsprache darf nicht leer sein.";
                return false;
            }
            if (string.IsNullOrEmpty(lan2))
            {
                error = "Die zweite Fremdsprache darf nicht leer sein.";
                return false;
            }
            if(g == GymType.SG) 
            {
                if(string.IsNullOrEmpty(lan3)) 
                {
                    error = "Die dritte Fremdsprache darf nicht leer sein.";
                    return false;
                }
            }
            else 
            {
                if (!string.IsNullOrEmpty(lan3))
                {
                    error = "Die dritte Fremdsprache darf nur in SG genutzt werden.";
                    return false;
                }
            }
            if(useLanLate == null) 
            {
                error = "Die Auswahl \"Fremdsprache abwählen?\" darf nicht leer sein.";
                return false;
            }
            else 
            {
                if((bool)useLanLate) 
                {
                    if (replaceLan1 == null)
                    {
                        error = "Die Auswahl \"Fremdsprache 1 oder 2 abwählen?\" darf nicht leer sein.";
                        return false;
                    }
                    if (string.IsNullOrEmpty(lanLate))
                    {
                        error = "Die spätbeginnende Fremdsprache darf nicht leer sein.";
                        return false;
                    }
                }
                else 
                {
                    if (replaceLan1 != null)
                    {
                        error = "Die Auswahl \"Fremdsprache 1 oder 2 abwählen?\" darf nur benutzt werden, wenn eine Fremdsprache abgelegt wird.";
                        return false;
                    }
                    if (!string.IsNullOrEmpty(lanLate))
                    {
                        error = "Die spätbeginnende Fremdsprache darf nicht nicht benutzt werden, wenn keine Fremdsprache abgelegt wird.";
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}
