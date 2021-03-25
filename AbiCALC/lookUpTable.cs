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
            for (int x = 0; x < 28; x++)
            {
                i -= 18;
                upperBounds.Add(i);
            }
        }

        private int _getAbi(int i) 
        {
            throw new NotImplementedException();
        }

        public static int getAbi(int points) => (singleton ??= new lookUpTable())._getAbi(points);

        public static string getAbiGrade(int abi) => $"{abi / 10},{abi % 10}";
        
    }
}
