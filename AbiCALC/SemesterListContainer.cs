using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class SemesterListContainer
    {
        private static SemesterListContainer _s;
        public static SemesterListContainer singleton 
        {
            get 
            {
                return _s ??= new SemesterListContainer();
            }
            set => _s = value;
        }
        public List<semester> semesters = new List<semester>();
        [OnDeserialized]
        public void deserialized(StreamingContext context)
        {
            singleton = this;
        }
    }
}
