using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ai = 1000;
            const int aj = 1200;
            const int bi = 1200;
            const int bj = 1000;

            int[,] matrA = new int[ai,aj];
            int[,] matrB = new int[bi,bj];


            FillMatrixRnd(matrA, 30);
            FillMatrixRnd(matrB, 30);
            MultMatr(matrA, matrB);

            //jagged arrays;

            int[][] matrC = new int[ai][];
            for (int i = 0; i < ai; i++) { matrC[i] = new int[aj]; }
            int[][] matrD = new int[bi][];
            for (int i = 0; i < bi; i++) { matrD[i] = new int[bj]; }

            MultMatrJagged(matrC, matrD);
            Console.ReadKey();
        }

        static void FillMatrixRnd(int[,] matrInts, int maxnum = 100)
        {
            Random rnd = new Random();
            for (int i = 0; i < matrInts.GetLength(0); i++)
            {
                for (int j = 0; j < matrInts.GetLength(1); j++)
                {
                    matrInts[i,j] = (int)(rnd.NextDouble()*maxnum);
                }
            }
        }

        static void FillMatrixJaggedRnd(int[][] matrInts, int maxnum = 100)
        {
            Random rnd = new Random();
            for (int i = 0; i < matrInts.Length; i++)
            {
                for (int j = 0; j < matrInts[i].Length; j++)
                {
                    matrInts[i][j] = (int)(rnd.NextDouble() * maxnum);
                }
            }
        }

        //print 2d matr
        static void PrintMatrix(int[,] matrInts)
        {
         
            for (int i = 0; i < matrInts.GetLength(0); i++)
            {
                for (int j = 0; j < matrInts.GetLength(1); j++)
                {
                    Console.Write($"{matrInts[i,j]}, ");
                }
                Console.WriteLine("\n");
            }
        }

        //mult 2d matr
        public static int[,] MultMatr(int[,] matrA, int[,] matrB)
        {
            if (matrA.GetLength(0) != matrB.GetLength(1)) throw new Exception();
            int[,]matrResult = new int[matrA.GetLength(0), matrB.GetLength(1)];

            Stopwatch sw = new Stopwatch();
            sw.Start();


            for (int i = 0; i < matrB.GetLength(1); i++)
            {
                for (int j = 0; j < matrB.GetLength(1); j++)
                {
                    //для каждого элемента
                    int sum = 0;
                    for (int k = 0; k < matrB.GetLength(1); k++)
                    {
                        sum += matrA[i, k] * matrB[k, j];
                    }
                    matrResult[i, j] = sum;
                }
            }
            sw.Stop();
            long duration = sw.ElapsedMilliseconds;
            Console.WriteLine($"умножили [{matrA.GetLength(0)}*{matrA.GetLength(1)}] * [{matrB.GetLength(0)}*{matrB.GetLength(1)}] за {duration} мс");
            long flops = ((long)(2*matrA.GetLength(0)*matrA.GetLength(0)*matrA.GetLength(0))* 1000 / duration);
            Console.WriteLine($"производительность {flops/1000000} ГФЛОПс");
            return matrResult;
        }

        public static int[][] MultMatrJagged(int[][] matrA, int[][] matrB)
        {
            if (matrA.Length != matrB[0].Length) throw new Exception();

            int[][] matrResult = new int[matrA.Length][];

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < matrA.Length; i++)
            {
                matrResult[i] = new int[matrA.Length];
                for (int j = 0; j < matrA.Length; j++)
                {
                    //each element
                    int sum = 0;
                    for (int k = 0; k < matrA.Length; k++)
                    {
                        sum += matrA[i][k]*matrB[k][j];
                    }
                    matrResult[i][j] = sum;
                }
            }
            sw.Stop();
            long duration = sw.ElapsedMilliseconds;
            Console.WriteLine($"умножили [{matrA.Length}*{matrA[0].Length}] * [{matrB.Length}*{matrB[0].Length}] за {duration} мс");
            long flops = ((long)(2 * matrA.Length * matrA.Length * matrA.Length) * 1000 / duration);
            Console.WriteLine($"производительность {flops / 1000000} ГФЛОПс");

            return matrResult;
        }
    }
}
