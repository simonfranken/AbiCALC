using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class overrideDictContainer
    {
        private static overrideDictContainer _singleton = null;
        public static overrideDictContainer singleton 
        {
            get 
            {
                return _singleton ??= new overrideDictContainer();
            }
            set => _singleton = value;
        }
        public Dictionary<string, (subjectTypes, subjectTypes, int, int)> overrideDict = new Dictionary<string, (subjectTypes, subjectTypes, int, int)>();
        [OnDeserialized]
        public void deserialized(StreamingContext context)
        {
            singleton = this;
        }
        public overrideDictContainer() 
        {
            deserialized(default);
        }
    }
}
