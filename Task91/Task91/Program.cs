using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task91
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "task21.exe")))
            {
                Assembly asm = Assembly.Load("Task21");

                Type mycomplexType = asm.GetType("task21.Complex");
                object x = Activator.CreateInstance(mycomplexType, 2, 3);

                MethodInfo byarg = mycomplexType.GetMethod("ByArg");
                object y = byarg.Invoke(null, new object[] {14, Math.PI/4});

        
                MethodInfo complAdd = mycomplexType.GetMethod("op_Addition");
                MethodInfo complMul = mycomplexType.GetMethod("op_Multiply");
                MethodInfo complDiv = mycomplexType.GetMethod("op_Division");
                MethodInfo complTostring = mycomplexType.GetMethod("ToString");
                MethodInfo compGetabs = mycomplexType.GetMethod("get_Abs");
                MethodInfo compGetarg = mycomplexType.GetMethod("get_Arg");

                object tmp = complAdd.Invoke(null, new object[] {x, y});
                object tmp2 = complMul.Invoke(null, new object[] {tmp, tmp});
                object tmp3 = Activator.CreateInstance(mycomplexType, (double)27);
                object z = complDiv.Invoke(null, new object[] {tmp2,  tmp3});
                Console.WriteLine($" z ={complTostring.Invoke(z, new object[] {})}");
                Console.WriteLine($" z.mod = {compGetabs.Invoke(z, null)} z.arg= {compGetarg.Invoke(z, null)}");

                //-------

                
                dynamic a = Activator.CreateInstance(mycomplexType, 4, 1);
                dynamic b = byarg.Invoke(null, new object[] {2, Math.PI/3});

                dynamic t = a*a + b*b;

                dynamic c = (t*t)/(3*b);

                Console.WriteLine($"c = {c.ToString()}");
                Console.WriteLine($"c.abs = {c.Abs} c.arg = {c.Arg}");                
            }
            else
            {
                Console.WriteLine("отсутстует task21.exe");
                
                Complex e = new Complex (1, 2);
                Complex f = Complex.FromPolarCoordinates(3, Math.PI/8);
                Complex d = 34 + Complex.Pow(e, f);
                Console.WriteLine($"d = {d}");
                
            }
            Console.ReadLine();
        }
    }
}
