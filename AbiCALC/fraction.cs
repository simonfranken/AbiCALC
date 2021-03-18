using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    class fraction
    {
        private int numerator;
        private int denuminator;



        //constructors
        public fraction()
        {
            numerator = 0;
            denuminator = 1;
        }

        public fraction(int integer)
        {
            numerator = integer;
            denuminator = 1;
        }

        public fraction(int numerator, int denuminator)
        {
            this.numerator = numerator;
            this.denuminator = denuminator;
        }

        public fraction(int integer, int numerator, int denuminator)
        {
            this.denuminator = denuminator;
            this.numerator = (integer * denuminator) + numerator;
        }



        //get-methods
        public string getString()
        {
            return numerator + " / " + denuminator;
        }



        //public methods
        public void add(fraction summand)
        {
            fraction result = addFractions(this, summand);
            this.numerator = result.numerator;
            this.denuminator = result.denuminator;
        }

        public void subtract(fraction subtractor)
        {
            fraction result = subtractFractions(this, subtractor);
            this.numerator = result.numerator;
            this.denuminator = result.denuminator;
        }

        public void multiply(fraction factor)
        {
            fraction result = multiplyFractions(this, factor);
            this.numerator = result.numerator;
            this.denuminator = result.denuminator;
        }

        public void divide(fraction divisor)
        {
            fraction result = divideFractions(this, divisor);
            this.numerator = result.numerator;
            this.denuminator = result.denuminator;
        }



        //public static methods
        static public fraction addFractions(fraction summand1, fraction summand2)
        {
            summand1.denuminator *= summand2.denuminator;
            summand1.numerator = (summand1.numerator * summand2.denuminator) + (summand2.numerator * summand1.denuminator);
            return shorten(summand1);

        }

        static public fraction subtractFractions(fraction subtrahend, fraction subtractor)
        {
            subtrahend.denuminator *= subtractor.denuminator;
            subtrahend.numerator = (subtrahend.numerator * subtractor.denuminator) - (subtractor.numerator * subtrahend.denuminator);
            return shorten(subtrahend);
        }

        static public fraction multiplyFractions(fraction factor1, fraction factor2)
        {
            factor1.numerator *= factor2.numerator;
            factor1.denuminator *= factor2.denuminator;
            return shorten(factor1);
        }

        static public fraction divideFractions(fraction divident, fraction divisor)
        {
            divident.numerator /= divisor.denuminator;
            divident.denuminator /= divisor.numerator;
            return shorten(divident);
        }



        //private static methods
        static private fraction shorten(fraction f)
        {
            int ggd = getGGD(f.numerator, f.denuminator);

            f.numerator /= ggd;
            f.denuminator /= ggd;

            return f;
        }

        static private int getGGD(int a, int b)
        {
            if (a == b)
            {
                return a;
            }

            if (a > b)
            {
                int r = a % b;
                if (r == 0)
                {
                    return b;
                }
                else
                {
                    return getGGD(b, r);
                }
            }

            else 
            {
                int r = b % a;
                if (r == 0)
                {
                    return a;
                }
                else
                {
                    return getGGD(a, r);
                }
            }
        }
    }
}
