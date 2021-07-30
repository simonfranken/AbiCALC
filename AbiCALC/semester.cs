using System;
using System.Collections.Generic;
using lib.interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class semester : IName
    {
        public Dictionary<subjectTypes, subject> dict = new Dictionary<subjectTypes, subject>();
        public observableItem<string> Name => name;
        public observableItem<string> name = new observableItem<string>();
        private SemesterListContainer slc;
        public semester(string s) 
        {
            slc = SemesterListContainer.singleton;
            slc.semesters.Add(this);
            name.itemValue = s;
        }

        public semester() : this(getName()) { }

        private static int count = -1;
        private static string getName() 
        {
            count++;
            string s =  string.Format("{0}/{1}", (count / 2) + 11, (count % 2) + 1);
            return s;
        }
    }
}
