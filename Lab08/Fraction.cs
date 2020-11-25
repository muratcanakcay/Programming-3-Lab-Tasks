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
            get
            { return numerator; }
            set
            {
                numerator = value;
                simplify();
            }
        }

        public long Denominator
        {
            get
            { return denominator; }
            set
            {
                if (value <= 0) throw new ArgumentException("Denominator cannot be 0 or negative");
                denominator = value;
                simplify();
            }
        }

        public Fraction(long n, long d)
        {
            if (d <= 0) throw new ArgumentException("Denominator cannot be 0 or negative");

            numerator = n;
            denominator = d;
            simplify();
        }

        public Fraction(long n)
        {
            numerator = n;
            denominator = 1;
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
                var oldd = d;
                d = n % d;
                n = oldd;
            }
            return n;
        }

        void simplify()
        {
            long gcd = GetGCD(numerator, denominator);            
            numerator /= gcd;
            denominator /= gcd;
            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }
        }

        public string Reciprocal()
        {
            long temp = denominator;

            denominator = numerator;
            numerator = temp;
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator *= -1;
            }

            simplify();

            return this.ToString();
        }

        //- implicit from long type to Fraction
        //- explicit from Fraction to long
        //- explicit from Fraction to double

        public static implicit operator Fraction(long l)
        {
            return new Fraction(l);
        }

        public static explicit operator long(Fraction f)
        {
            return f.Numerator / f.Denominator;
        }

        public static explicit operator double(Fraction f)
        {
            return (double)f.Numerator / (double)f.Denominator;
        }

        //+ - * / and unary -

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction();

            ret.numerator = (f1.numerator * f2.denominator + f1.denominator * f2.numerator);
            ret.denominator = (f1.denominator * f2.denominator);

            ret.simplify();

            return ret;
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction();

            ret.numerator = (f1.numerator * f2.denominator - f1.denominator * f2.numerator);
            ret.denominator = (f1.denominator * f2.denominator);

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

            ret.numerator = (f1.numerator * f2.numerator);
            ret.denominator = (f1.denominator * f2.denominator);

            ret.simplify();

            return ret;
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction();

            ret.numerator = (f1.numerator * f2.denominator);
            ret.denominator = (f1.denominator * f2.numerator);

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
            return denominator.GetHashCode() ^ numerator.GetHashCode();
        }

    }
}