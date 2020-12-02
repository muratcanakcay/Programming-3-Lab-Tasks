using System;

namespace Lab8
{
    public struct Fraction
    {
        private long numerator;
        private long denominator;

        // TODO: Implement properties and constructor

        public long Numerator
        {
            get => numerator; 
            set
            {
                numerator = value;
                if (denominator != 0) simplify();
            }
        }

        public long Denominator
        {
            get => denominator; 
            set
            {
                if (value <= 0) throw new ArgumentException("Denominator cannot be 0 or negative");
                denominator = value;
                simplify();
            }
        }

        public Fraction(long n, long d = 1) : this()
        {   
            Numerator = n;
            Denominator = d;            
        }
        
        public override string ToString()
        {
            var whole = numerator / denominator;
            var num = numerator - whole * denominator;
            var sign = numerator > 0;

            var str = string.Empty;
            if (!sign)
            {
                str += "-";
                num = -num;
                whole = -whole;
            }
            if (num == 0)
                str += $"[{whole}]";
            else if (whole != 0)
                str += $"[{whole} {num}/{denominator}]";
            else
                str += $"[{num}/{denominator}]";

            return str;
        }

        // TODO: Implement all others methods, operators, etc.       

        static long GetGCD(long n, long d)
        {
            while (d != 0)
            {
                long x = d;
                d = n % d;
                n = x;
            }
            return n;
        }

        void simplify()
        {
            long gcd = Math.Abs(GetGCD(Numerator, Denominator));
            numerator /= gcd;
            denominator /= gcd;            
        }

        public Fraction Reciprocal()
        {
            if (Numerator < 0)
            {
                return new Fraction(-Denominator, -Numerator);
            }

            return new Fraction(Denominator, Numerator);
        }

        //- implicit from long type to Fraction
        //- explicit from Fraction to long
        //- explicit from Fraction to double

        public static implicit operator Fraction(long l) => new Fraction(l);

        public static explicit operator long(Fraction f) => f.Numerator / f.Denominator;

        public static explicit operator double(Fraction f) => (double)f.Numerator / (double)f.Denominator;
        
        //+ - * / and unary -

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction();

            ret.numerator = (f1.denominator == f2.denominator) ? (f1.numerator + f2.numerator) : ((f1.numerator * f2.denominator) + (f1.denominator * f2.numerator));
            ret.denominator = (f1.denominator == f2.denominator) ? (f1.denominator) : (f1.denominator * f2.denominator);

            ret.simplify();

            return ret;
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction();

            ret.numerator = (f1.denominator == f2.denominator) ? (f1.numerator + f2.numerator) : ((f1.numerator * f2.denominator) - (f1.denominator * f2.numerator));
            ret.denominator = (f1.denominator == f2.denominator) ? (f1.denominator) : (f1.denominator * f2.denominator); 

            ret.simplify();

            return ret;
        }

        public static Fraction operator -(Fraction f)
        {
            Fraction ret = new Fraction();

            ret.numerator = -f.numerator;
            ret.denominator = f.denominator;

            ret.simplify();

            return ret;
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction();

            long gcd1 = GetGCD(f1.numerator, f2.denominator);
            long gcd2 = GetGCD(f2.numerator, f1.denominator);

            ret.numerator = ((f1.numerator / gcd1) * (f2.numerator/gcd2));
            ret.denominator = ((f1.denominator / gcd2) * (f2.denominator / gcd1));

            ret.simplify();

            return ret;
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction();

            ret.Numerator = f1.denominator == f2.denominator ? f1.numerator : (f1.numerator * f2.denominator);
            ret.Denominator = f1.denominator == f2.denominator ? f2.numerator : (f1.denominator * f2.numerator);

            ret.simplify();
            
            return ret;
        }

        //== != < > <= >=

        public static bool operator ==(Fraction f1, Fraction f2)
        {
            return f1.numerator == f2.numerator && f1.denominator == f2.denominator;
        }

        public static bool operator !=(Fraction f1, Fraction f2)
        {
            return f1.numerator != f2.numerator || f1.denominator != f2.denominator;
        }

        public override bool Equals(object obj)
        {
            return this == (Fraction)obj;
        }

        public static bool operator <(Fraction f1, Fraction f2)
        {
            return f1.numerator * f2.denominator < f2.numerator * f1.denominator;
        }

        public static bool operator >(Fraction f1, Fraction f2)
        {
            return f1.numerator * f2.denominator > f2.numerator * f1.denominator;
        }

        public static bool operator <=(Fraction f1, Fraction f2)
        {
            return f1<f2 || f1==f2;
        }

        public static bool operator >=(Fraction f1, Fraction f2)
        {
            return f1 > f2 || f1 == f2;
        }

        public override int GetHashCode()
        {
            return Denominator.GetHashCode() ^ Numerator.GetHashCode();
        }

    }
}