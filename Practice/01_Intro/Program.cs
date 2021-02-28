using System; // static is not used because System is a namespace
using static System.Console; // static is used because Console is a type

class GlobalClass { }


namespace Intro
{

    class Test
    {
        void func()
        {
            int a = 1;
            int b, c, d;
            int[] t1, t2;
            b = 2;
            this.e = 0; // instance.type
            Intro.Test.f = 0; // namespace.type.type
            if (a < 2) c = 3;
            t1 = new int[5];
            d = a;
            d = b;

            d = t1[0];
            //d = t2[0]; // error - not assigned
        }

        int e;
        static int f;
        global::GlobalClass GC; // from global namespace

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
