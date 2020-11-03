
using System;

namespace Example
{

class SecondProgram
    {
    const int SIZE = 4;

    public static void Main()
        {
        double[] x;
        int i;
        string buf;
        x = new double[SIZE];
        Console.WriteLine("Input vector coeficients");
        //  1  5  3  -1
        for ( i=0 ; i<x.Length ; ++i )
            {
            Console.Write("coeficient {0}:  ",i);
            buf = Console.ReadLine();
            x[i] = double.Parse(buf);
            }
        double s = 0.0;
        for ( i=0 ; i<x.Length ; ++i )
            s += x[i]*x[i];
        s = Math.Sqrt(s);
        Console.WriteLine("\nVector length is {0,8:0.000}",s);
        }

    }  // class SecondProgram

}  // namespace Example
