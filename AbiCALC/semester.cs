using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class semester
    {
        public Dictionary<subjectTypes, subject> dict = new Dictionary<subjectTypes, subject>();
    }
}
