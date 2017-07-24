using System;
using System.Linq;

namespace BeDivisible
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw;

            for (var i = 10; i <= 20; i++)
            {
                Console.WriteLine("n=" + i);
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                Console.WriteLine("答え:" + GetMin(i));
                sw.Stop();
                Console.WriteLine("経過時間:" + sw.Elapsed + Environment.NewLine);
            }

            for (var i = 10; i <= 20; i++)
            {
                Console.WriteLine("n=" + i);
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                Console.WriteLine("答え:" + GetLeastCommonMultiple(i));
                sw.Stop();
                Console.WriteLine("経過時間:" + sw.Elapsed + Environment.NewLine);
            }


            while (true) { }
        }

        static int GetMin(int n)
        {
            return Enumerable.Range(1, int.MaxValue).Where(x=>Enumerable.Range(1, n).All(y=>x%y==0)).FirstOrDefault();
        }

        static int GetLeastCommonMultiple(int n)
        {
            return Enumerable.Range(1, int.MaxValue).Except(Enumerable.Range(1, int.MaxValue).Where(x => Enumerable.Range(1, n).Any(y => x % y != 0))).FirstOrDefault();
        }
    }
}
