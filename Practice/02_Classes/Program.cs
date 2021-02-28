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
        const int c = 0; // always static
        readonly int d; // can be instance field or static, assigned in c-tor or class declaration
        static readonly int e; 
        
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
            // B.n = 1; // error, n is instance member  - requires instance reference (this.n = ...)
            this.n = 1; // correct
            
            // b.x = 2; // error, x is static member - requires class name reference (B.x = ...)
            // correct in C++
            B.x = 2; // correct
        }
    } // class B

    // inheritance
    // derived class contains all members of base class except c-tors and finalizers

    public class BB
    {
        public int x = 1;
        public virtual void fun() { Console.WriteLine("BB {0}", x); }
    }
    public class DD : BB
    {
        public int x = 2; // hides inherited member
        public override void fun() // overrides inherited method
        {
            base.fun(); // executes base class member fun()
            Console.WriteLine("DD {0} {1}", x, base.x);           
        }
    }
    public class EE : BB
    {
        public int x = 3; // hides inherited member
        public new void fun() // hides inherited method
        {
            base.fun(); // executes base class member fun()
            Console.WriteLine("EE {0} {1}", x, base.x);
        }
    }

    public class FF : DD
    {
        public int x = 4; // hides inherited member
        public new virtual void fun() // creates new virtual chain for method inherited from BB to DD
        {
            base.fun(); // executes base class member fun()
            Console.WriteLine("FF {0} {1}", x, base.x);
        }
    }


    // abstract classes

    public abstract class AAA // can have both abstract and non-abstract methods
    {
        public abstract void fun(); // can only appear in abstract classes
        public void fun2()
        {
            Console.WriteLine();
        }
    }

    public class BBB : AAA // must override all of the abstract methods of the base abstract class AAA 
    {
        public override void fun() 
        {
            Console.WriteLine("virtual BBB");
        }
    }

    // -----------

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            BB b = new BB();
            b.fun(); // uses BB.fun()
            Console.WriteLine(); 
            
            DD d = new DD();
            d.fun(); // uses DD.fun()
            Console.WriteLine();

            EE e = new EE();
            e.fun(); // uses EE.fun()
            Console.WriteLine();

            BB bDD = new DD();
            bDD.fun(); // overrides BB.fun() and uses DD.fun() even though its class is BB
            Console.WriteLine();

            BB bEE = new EE();
            bEE.fun(); // DOES NOT override and uses BB.fun() since its class is BB and -new- not -override- was used in EE.
            Console.WriteLine();

            FF f = new FF();
            f.fun(); // uses FF.fun()
            Console.WriteLine();

            BB bFF = new FF();
            bFF.fun(); // DOES NOT override and uses DD.fun() since its class is BB, however DD.fun() overrides BB.fun(), and -new virtual- not -override- was used in EE.
            Console.WriteLine();

            DD dFF = new FF();
            dFF.fun(); // DOES NOT override and uses DD.fun() since its class is BB and -new virtual- not -override- was used in EE.
            Console.WriteLine();
        }
    }
}
