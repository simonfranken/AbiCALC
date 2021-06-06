using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class baseSubjetTypes
    {
        public static readonly baseSubjetTypes W = new baseSubjetTypes("W");
        public static readonly baseSubjetTypes P = new baseSubjetTypes("P");
        public static readonly baseSubjetTypes D = new baseSubjetTypes("D");

        public string t;
        public baseSubjetTypes(string s) 
        {
            t = s;
        }

        public List<string> getHeadings() 
        {
            switch(this.t) 
            {
                case "W":
                    return new List<string> {"1", "2", "Arbeit 1", "Arbeit 2" };
                case "P":
                    return new List<string> { "1", "2" };
                case "D":
                    List<string> ret = new List<string>();
                    foreach (var item in SemesterListContainer.singleton.semesters)
                    {
                        ret.Add(item.Name.itemValue);
                    }
                    ret.Add("Abi");
                    return ret;
                default:
                    throw new Exception("baseSubjectTypes not found");
            }
        }
    }
}
