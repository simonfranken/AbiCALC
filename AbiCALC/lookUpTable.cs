using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public class lookUpTable
    {
        private static lookUpTable singleton;
        private List<int> upperBounds = new List<int>();

        private lookUpTable() 
        {
            int i = 900;
            upperBounds.Add(i);
            i -= 78;
            upperBounds.Add(i); 
            for (int x = 0; x < 29; x++)
            {
                i -= 18;
                upperBounds.Add(i);
            }
        }

        private int _getAbi(int i) 
        {
            if (i < 300) return -1;
            int p = 9;
            foreach (int max in upperBounds)
            {
                if (i <= max) p++;
                else break;
            }
            return p;
        }

        public static int getAbi(int points) => (singleton ??= new lookUpTable())._getAbi(points);

        public static string getAbiGrade(int abi) => abi != -1 ? $"{abi / 10},{abi % 10}" : "nicht bestanden";
        
    }
}
