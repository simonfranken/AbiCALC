using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class mins
    {
        public Dictionary<subjectTypes, int> m = new Dictionary<subjectTypes, int>();
        
        public mins()
        {
            m[subjectTypes.Deutsch] = 4;
            m[subjectTypes.Mathe] = 4;

            m[subjectTypes.Sport] = 0;
            m[subjectTypes.Religion] = 3;

            m[subjectTypes.Geschichte] = 3;
            m[subjectTypes.Sozialkunde] = 3;
            
            m[subjectTypes.Geographie] = 3;
            m[subjectTypes.Wirtschaft] = 3;
            m[subjectTypes.Wirtschaftsinformatik] = 0;

            m[subjectTypes.Kunst] = 3;
            m[subjectTypes.Musik] = 3;

            m[subjectTypes.Englisch] = 1;
            m[subjectTypes.Franzoesisch] = 1;
            m[subjectTypes.Latein] = 1;
            m[subjectTypes.Altgriechisch] = 1;
            m[subjectTypes.Russisch] = 1;
            m[subjectTypes.Spanisch] = 1;
            m[subjectTypes.Tuerkisch] = 1;
            
            m[subjectTypes.Biologie] = 1;
            m[subjectTypes.Physik] = 1;
            m[subjectTypes.Biophysik] = 1;
            m[subjectTypes.Astrophysik] = 1;
            m[subjectTypes.Chemie] = 1;
            m[subjectTypes.Informatik] = 1;
        }
    }
}
