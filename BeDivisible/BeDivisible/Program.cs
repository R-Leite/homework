using System;
using System.Collections.Generic;
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
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                var answer = LeastCommonMultiple(i).ToString().PadLeft(10, ' ');
                sw.Stop();
                Console.WriteLine("1-" + i + " : 答え = " + answer + " : 処理時間 = " + sw.Elapsed);
            }

            for (var i = 10; i <= 20; i++)
            {
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                var answer = AllDivisible(i).ToString().PadLeft(10, ' ');
                sw.Stop();
                Console.WriteLine("1-" + i + " : 答え = " + answer + " : 処理時間 = " + sw.Elapsed);
            }

            while (true) { }
        }

        static int AllDivisible(int n)
        {
            return Enumerable.Range(1, int.MaxValue).Where(x => x % n == 0).Where(x => Enumerable.Range(1, n).All(y => x % y == 0)).FirstOrDefault();
        }

        static int LeastCommonMultiple(int n)
        {
            return Enumerable.Range(1, n).Aggregate((lcm, next) => Enumerable.Range(1, next).Select(x => x * lcm).Where(mul => mul % next == 0).FirstOrDefault());
        }
    }
}
