﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class subjectTypes
    {
        public subjectTypes(type _t) : this(_t, _t.ToString()) { }

        public subjectTypes(type _t, string s)
        {
            t = _t;
            name = s;
        }


        public string name;
        public type t;
        public enum type 
        {
            Naturwissenschaft, Fremdsprache, Deutsch, Mathe, KunstMusik, GeoWirtschaft, Profil, WSem, PSem, Reli, GeschichteSozi, Sport, Geschichte, Sozi, Informatik
        }
        public static readonly subjectTypes Deutsch = new subjectTypes(type.Deutsch);
        public static readonly subjectTypes Mathe = new subjectTypes(type.Mathe);
        public static readonly subjectTypes Sport = new subjectTypes(type.Sport);
        public static readonly subjectTypes Reli = new subjectTypes(type.Reli);
        public static readonly subjectTypes KunstOderMusik = new subjectTypes(type.KunstMusik);
        public static readonly subjectTypes GeoOderWirtschaft = new subjectTypes(type.GeoWirtschaft);
        public static readonly subjectTypes WS = new subjectTypes(type.WSem);
        public static readonly subjectTypes PS = new subjectTypes(type.PSem);
       

        ////Fremdsprachen



        ////Naturwissenschaften


        ////Die Sendung mit Herr Kraus
        //Informatik,
    }
}
