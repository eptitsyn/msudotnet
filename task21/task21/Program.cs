using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace task21
{
    public class Complex
    {
        double Re { get; set; }
        double Im { get; set; }
        public double Abs
        {
            get
            {
                return Math.Sqrt(Re * Re + Im * Im);
            }
        }
        public double Arg
        {
            get
            {
                return Math.Atan((double)Im / Re);
            }
        }
        
        public Complex(double a)
        {
            Re = a;
        }
        
        public Complex(double x, double ix = 0)
        {
            Re = x;
            Im = ix;
        }
        //--- операторы
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Re + b.Re, a.Im + b.Im);
        }
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Re - b.Re, a.Im - b.Im);
        }
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.Re * b.Re - a.Im * b.Im, a.Re * b.Im + b.Re * a.Im);
        }
        public static Complex operator /(Complex a, Complex b)
        {
            return new Complex((a.Re * b.Re + a.Im * b.Im) / (b.Re * b.Re + b.Im * b.Im), (a.Im * b.Re - a.Re * b.Im) / (b.Re * b.Re + b.Im * b.Im));
        }
        //-- сравнения
        public static bool operator ==(Complex a, Complex b)
        {
            return (a.Re == b.Re) && (a.Im == b.Im);
        }
        public static bool operator !=(Complex a, Complex b)
        {
            return (a.Re != b.Re) || (a.Im != b.Im);
        }
        public override bool Equals(object o)
        {
            Complex a = o as Complex;
            return (a.Re == Re) && (a.Im == Im);
        }
        public override int GetHashCode()
        {
            throw new Exception("Sorry I don't know what GetHashCode should do for this class");
        }

        //-- в строку
        public override string ToString()
        {
            if (Im != 0)
            {
                if (Im > 0)
                {
                    return Re + "+i" + Im;
                }
                else
                    return Re.ToString() + "-i" + Math.Abs(Im);
            }
            else
                return Re.ToString();
        }

        //явное в int
        public static explicit operator double(Complex c)
        {
            return c.Re;
        }

        //неявное из инт
        public static implicit operator Complex(int a)
        {
            return new Complex(a);
        }

        public static Complex ByArg(double mod, double arg)
        {
            return new Complex((double)(mod * Math.Cos(arg)), (double)(mod * Math.Sin(arg)));
        }


    }
    class task2_1
    {
        static void Main(string[] args)
        {
            Complex SomeNum = new Complex(3, -7);
            Complex a = new Complex(5, 1);
            Complex b = new Complex(2, 1);

            Complex c = Complex.ByArg(2, 3);


            Console.WriteLine("a:" + a + "  b:" + b);
            Console.WriteLine("c:" + c);
            Console.WriteLine("a.arg:" + a.Arg);
            Console.WriteLine("a.mod:" + a.Abs);
            Console.WriteLine();
            Console.WriteLine("a+b:" + (a + b));
            Console.WriteLine("a-b:" + (a - b));
            Console.WriteLine("a*b:" + (a * b));
            Console.WriteLine("a/c:" + (a / c));
            Console.WriteLine("a == b:" + (a == b));
            c = b;
            Console.WriteLine("b == c:" + (b == c));

            int d = (int)new Complex(1, 3);
            Complex e = 3;

            Console.WriteLine("вывод:" + SomeNum);

            //Complex ay = Complex.ByArg(14, Math.PI/4);
            //Complex ax = new Complex(2,3);
            //Complex az = ax + ay;
            //az = az*az;
            //Console.WriteLine($"f: {az.ToString()}");
            Console.ReadKey();
        }
    }

}
