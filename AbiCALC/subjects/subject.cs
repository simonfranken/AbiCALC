using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public abstract class subject
    {
        public static subject constructor(subjectTypes t) 
        {
            switch(t.t) 
            {
                case subjectTypes.type.WSem:
                    return new subjects.wSemSubject();
                case subjectTypes.type.PSem:
                    return new subjects.pSemSubject();
                default:
                    return new normalSubject(t);

            }
        }
        
        public subjectTypes type;

        public subject(subjectTypes s, bool isNoAbi = true) 
        {
            type = s;
            if(isNoAbi) s.instances.Add(this);
            else 
            {
                s.a = this as abiexam;
            }
        }
    }
}
