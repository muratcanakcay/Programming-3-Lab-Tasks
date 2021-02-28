using System;

namespace Classes
{
    // static class
    public static class A
    {
        // only static members
        static public int staticMember;
        // int public nonStaticMember; // gives error
    } // class A

    // "ordinary" class
    class B 
    {
        public int n;
        public static int x;
        
        public void fun1(B b)
        {
            n = b.n; // same as this.n = b.n
            B.x = 0; // same as x = 0;
        }
        
        public static void fun2(int i)
        {
            x = i; // same as B.x=i;
        }
        
        public void fun3(B b)
        {
            // B.n = 1; // error, n is instance member  - requires instance reference (this.x = ...)
            this.n = 1; // correct
            
            // b.x = 2; // error, x is static member - requires class name reference (B.x = ...)
            // correct in C++
            B.x = 2; // correct
        }
    } // class B

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
