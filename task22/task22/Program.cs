using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task22
{
    class Body3D
    {
        public int[] Edges;
        public virtual double Area { get; }
        public virtual double Volume { get; }
        public virtual int EdgesSum { get; }
    }

    class parallelepiped : Body3D
    {
        public parallelepiped(int a = 1, int b = 1, int c = 1)
        {
            Edges = new int[3];
            Edges[0] = a;
            Edges[1] = b;
            Edges[2] = c;
        }
        public override double Area => (Edges[0]* Edges[1] + Edges[1]* Edges[2] + Edges[0]* Edges[2]) * 2;
        public override double Volume => Edges[0] * Edges[1] * Edges[2];
        public override int EdgesSum => (Edges[0] + Edges[1] + Edges[2])*2;
    }
    class Sphere : Body3D
    {
        public Sphere(int radius = 1)
        {
            Edges = new int[1];
            Edges[0] = radius;
        }
        public override double Area => Edges[0] * Edges[0] * 4 * Math.PI;
        public override double Volume => Edges[0] * Edges[0] * Edges[0] * 4 * Math.PI  / 3;

    }
    class Tetrahedron : Body3D
    {
        public Tetrahedron(int Edge = 1)
        {
            Edges = new int[1];
            Edges[0] = Edge;
        }
        public override double Area => Edges[0] * Edges[0] * Math.Sqrt(3);
        public override double Volume => Edges[0] * Edges[0] * Edges[0] * Math.Sqrt(2) / 12;
        public override int EdgesSum => Edges[0] * 6;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Body3D[] arr = new Body3D[3];
            arr[0] = new Sphere(5);
            arr[1] = new Tetrahedron(3);
            arr[2] = new parallelepiped(2,3,4);
            Console.WriteLine($"Sphere r:{arr[0].Edges[0]} vol:{arr[0].Volume:N1} area:{arr[0].Area:N1}");
            Console.WriteLine($"Tetrahedron a:{arr[1].Edges[0]} vol:{arr[1].Volume:N1} area:{arr[1].Area:N1} edges sum:{arr[1].EdgesSum}");
            Console.WriteLine($"paralellepiped:{arr[2].Edges[0]} vol:{arr[2].Volume} area:{arr[2].Area} edges sum:{arr[2].EdgesSum}");
            Console.ReadKey();
        }
    }
}
