using System;
using System.Collections.Generic;

namespace Lab_10
{

    // ToDo : Write implementation here

    public static class Functions
    {
        public static Func<double, double> Constant(double constantValue)
        {            
            return (x) => constantValue;
        }

        public static Func<double, double> Identity()
        {
            return (x) => x;
        }

        public static Func<double, double> Exp(double coefficientValue)
        {
            return (x) => coefficientValue * Math.Exp(x);
        }
    }

    public class Function
    {
        private Func<double, double> func;
        
        private Func<double, double> Func
        {
            get => func;
            set => func = value;
        }

        public Function(Func<double, double> f)
        {
            Func = f;
        }

        public static implicit operator Function(Func<double, double> f) => new Function(f);

        public double Value(double x)
        {
            return this.Func(x);
        }

        public IEnumerable<double> GetValues(double a, double b, int n)
        {
            List<double> list = new List<double>();
            double step = (b - a) / n;

            for (int i = 0; i < n + 1; i++)
            {
                list.Add(Func(a + (i * step)));
            }            
            
            return list;
        }
    }

    public class Polynomial : Function
    {
        double[] coefficientValues;

        public Polynomial(double[] coefficientValues) : base(ToFunction(coefficientValues))
        {
            this.coefficientValues = coefficientValues;
        }

        public static Func<double, double> ToFunction(double[] coefficientValues)
        {
            Func<double, double> poly = delegate (double x)
            {
                double result = 0;
                
                for (int i = 0; i < coefficientValues.Length; i++)
                {
                    result += coefficientValues[i] * Math.Pow(x, i);    
                }

                return result;
            };

            return poly;            
        }

        public double Derivative(double x)
        {
            List<double> list = new List<double>();

            for (int i = 1; i < coefficientValues.Length; i++)
            {
                list.Add(i * coefficientValues[i]);
            }

            return ToFunction(list.ToArray())(x);
        }
    }

    public static class NumericalMethods
    {
        public static double Derivative(this Function f, double x, double h = 0.001)
        {
            double derivative = (f.Value(x + h) - f.Value(x - h)) / (2 * h);
            return derivative;
        }

        public static double Integral(this Function f, double a, double b, int n = 100)
        {
            double integral = 0;
            double h = (b - a) / n;            

            for (int i = 0; i < n; i++)
            {
                double xi = a + (i * h);                
                integral += h * f.Value(xi);
            }

            return integral;
        }
    }
}