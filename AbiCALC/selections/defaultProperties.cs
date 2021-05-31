using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.selections
{
    public class defaultProperties
    {
        //zweifachauswahl
        public selection.ReliType? rt = null;
        public selection.KunstOrMusikType? km = null;
        public selection.GeoOrWrType? gw = null;


        //namen
        public string WSemName = null, PSemName = null;

        //profil
        public Dictionary<string, bool> profil = new Dictionary<string, bool>();

        public bool isValid(ref string error)
        {
            if (string.IsNullOrEmpty(WSemName))
            {
                error = "Der W-Seminar-Name darf nicht leer sein.";
                return false;
            }
            if (string.IsNullOrEmpty(PSemName))
            {
                error = "Der P-Seminar-Name darf nicht leer sein.";
                return false;
            }
            if (rt == null)
            {
                error = "Bitte wähle eine Religion aus.";
                return false;
            }
            if (km == null)
            {
                error = "Biite wähle entweder Kunst oder Musik.";
                return false;
            }
            if (gw == null)
            {
                error = "Biite wähle entweder Geo oder Wirtschaft aus.";
                return false;
            }

            return true;
        }
    }
}
