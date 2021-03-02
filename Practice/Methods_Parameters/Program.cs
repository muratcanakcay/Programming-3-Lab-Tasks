using System;

namespace Methods_Parameters
{
    public class Test
    {
        public static void testing(int a, ref int b, out int c1, out int c2, in int d, int e = -5, params int[] tab)
        {
            a = 5; //will not have effect because a is valiue parameter
            b = 9; // works because reference is passed
            c1 = 666; // works because reference is passed, cannot ommit assignment because c is out parameter
            c2 = 666; // works because reference is passed, cannot ommit assignment because c is out parameter
            // d = 5; // error because d is in parameter
            // e is optional paramater. if it's not declared in method invocation default value is used

            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] *= tab[i];
            }


            Console.WriteLine($"a={a}, b={b}, c1={c1}, c2={c2}, d={d}, e={e}");
            foreach(var x in tab) Console.Write($"{x} ");
        }
    }

    class Program
    { 
        static void Main(string[] args)
        {
            int a = 0; // value - modifications in method do not affect original value
            int b = 0; // ref- can be modified in method but must be initialized 
            int c1; // out - does not have to be initialized but must be assigned inside method 
            // c2 can be declared in the method invocation because it's out parameter (can also be declared as var)
            int d = 0; // in - cannot be modified in method

            int[] t = { 1, 2, 3 }; // method's last paramater must have params modifier
                                   // values of array can be modified in method
                                   // since there's an optional paramater e before params t we must name it with t:t in method invocation 

            Test.testing(a, ref b, out c1, out int c2, in d, tab:t);

            
        }
    }
}
