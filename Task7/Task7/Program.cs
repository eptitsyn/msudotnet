using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    class Program
    {
        public delegate double FuncDelegate(double x);

        static void Main(string[] args)
        {
            FuncDelegate func = new FuncDelegate(F1);
            double res = ReverseFunc(0, 1, func, 0.5, 0.0001);
            Console.WriteLine($" x = {res}");

            func = delegate(double x) { return x*x + Math.Sin(x - 2); };
            res = ReverseFunc(2.5, 3.5, func, 8, 0.0001);
            Console.WriteLine($" x = {res}");
            

            func = x => Math.Pow(Math.E, x)*Math.Sin(x);
            res = ReverseFunc(0.1, 2.35, func, 6, 0.00001);
            Console.WriteLine($" x = {res}");
            Console.ReadKey();
        }

        static double F1(double x)
        {
            return Math.Sin(x);
        }
        

        static double ReverseFunc(double a, double b, FuncDelegate f, double y, double eps)
        {
            double result = (a + b) / 2;
            double step = (b - a) / 2;
            while (Math.Abs(f(result)-y) > eps)
            {
                Console.WriteLine($"Eps = {Math.Abs(f(result) - y)}");
                if ((Math.Abs(f(result) - y) > Math.Abs(f(result+step) - y)) && (result+step < b))
                {
                    result += step;
                }
                if ((Math.Abs(f(result) - y) > Math.Abs(f(result - step) - y)) && (result - step > a))
                {
                    result -= step;
                }
                step = step/2;
            }
            Console.WriteLine($"Eps = {Math.Abs(f(result) - y)}");
            return result;
        }
    }
}
