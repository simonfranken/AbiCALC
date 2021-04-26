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

        //set
        

        private void set(fraction f) => set(f.numerator, f.denominator);
        private void set(int num, int den) 
        {
            numerator = num;
            denominator = den;
            shorten();
        }

        //constructors

        public fraction(int integer) => set(integer, 1);

        public fraction(int n, int d) => set(n, d);

        public fraction(int integer, int numerator, int denominator) => set((integer * denominator) + numerator, denominator);

        public fraction(fraction f) => set(f);

        //shorten
        private void shorten()
        {
            int ggd = getGGD(numerator, denominator);

            numerator /= ggd;
            denominator /= ggd;
        }

        public bool isDivZeroError() => denominator == 0;

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



        //operators arithmetic
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



        //operators comparison
        public static bool operator ==(fraction a, fraction b) => a.Equals(b);

        public static bool operator !=(fraction a, fraction b) => !(a == b);



        //operators cast

        public static explicit operator fraction(int i) => new fraction(i);

        public static implicit operator int?(fraction f) => f.rounded();


        //methods

        public int? rounded()
        {
            if (isDivZeroError()) return null;
            int r = numerator / denominator;
            int rest = numerator % denominator;
            if(2*rest >= denominator) 
            {
                r++;
            }
            return r;
        }

        public void round() 
        {
            int? i = rounded();
            if (i != null) set((int)i, 1);
            else set(0, 0);
        }

        public float getValue() 
        {
            return ((float)numerator) / ((float)denominator);
        }

        public fraction round2Decimals() 
        {
            int i = (int)Math.Floor((getValue() * 100));
            return new fraction(i, 100);
        }

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
                return 1;
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
