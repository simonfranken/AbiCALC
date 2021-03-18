using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public class fraction
    {
        private int numerator;
        private int denominator;



        //constructors
        public fraction(fraction f)
        {
            set(f);
            shorten();
        }

        private void set(fraction f) 
        {
            numerator = f.numerator;
            denominator = f.denominator;
            shorten();
        }

        public fraction(int integer)
        {
            numerator = integer;
            denominator = 1;
            shorten();
        }

        public fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            shorten();
        }

        public fraction(int integer, int numerator, int denominator)
        {
            this.denominator = denominator;
            this.numerator = (integer * denominator) + numerator;
            shorten();
        }

        private void shorten()
        {
            int ggd = getGGD(numerator, denominator);

            numerator /= ggd;
            denominator /= ggd;
        }

        public fraction getInverse() => fraction.getInverse(this);

        //overrides
        public override string ToString() => $"{numerator} / {denominator}";
        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType()) 
            {
                fraction f = (fraction)obj;
                shorten();
                f.shorten();
                return numerator == f.numerator && denominator == f.denominator;
            }
            return false;
        }



        //operators
        public static fraction operator +(fraction a) => a;
        public static fraction operator -(fraction a) => new fraction(-a.numerator, a.denominator);

        public static fraction operator +(fraction a, fraction b)
            => new fraction(a.numerator * b.denominator + b.numerator * a.denominator, a.denominator * b.denominator);

        public static fraction operator -(fraction a, fraction b)
            => a + (-b);

        public static fraction operator *(fraction a, fraction b)
            => new fraction(a.numerator * b.numerator, a.denominator * b.denominator);

        public static fraction operator /(fraction a, fraction b)
            => a * b.getInverse();

        //public static methods

        public static fraction getInverse(fraction f) 
        {
            return new fraction(f.denominator, f.numerator);
        }

        //private static methods

        static private int getGGD(int a, int b)
        {
            if(a == 0 || b == 0) 
            {
                if (a == 0) return b;               
                else return a;
            }

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
