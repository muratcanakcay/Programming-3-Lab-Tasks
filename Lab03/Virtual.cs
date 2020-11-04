using System;

namespace Lab03
{
    class B
    {
        public virtual void fun_v1() { Console.WriteLine("B_V1"); }
        public virtual void fun_v2() { Console.WriteLine("B_V2"); }
        public void fun_nv() { Console.WriteLine("B_NV");  }
    }

    class D : B
    {
        public override void fun_v1() { Console.WriteLine("D_V1"); }
        public override void fun_v2() { Console.WriteLine("D_V2"); }
        public /*new*/ void fun_nv() { Console.WriteLine("D_NV"); }             // warning - it can be suppressed by using new modifier
    }

    class E : D
    {
        public override void fun_v1() { Console.WriteLine("E_V1"); }
        public /*new*/ virtual void fun_v2() { Console.WriteLine("E_V2"); }     // warning - it can be suppressed by using new modifier
        public /*new*/ void fun_nv() { Console.WriteLine("E_NV"); }             // warning - it can be suppressed by using new modifier
    }
}
