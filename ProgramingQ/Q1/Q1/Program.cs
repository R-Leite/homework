using System;
using System.Linq;

namespace Q1
{
    class Program
    {
        const int CUTCOUNT = 40;

        static void Main(string[] args)
        {
            var lines = Fibonacci(CUTCOUNT);
            Console.WriteLine("縦の長さ：" + lines.Item1);
            Console.WriteLine("横の長さ：" + lines.Item2);
            Console.ReadKey();
        }

        static Tuple<long, long> Fibonacci(int n)
        {
            // フィボナッチ数列の作成
            var fibonacci = Enumerable.Repeat(new[] { 2L, 1L }, n + 1).Select(x => x[1] = (x[0] = x[0] + x[1]) - x[1]).Reverse().ToList();
            Console.WriteLine(fibonacci.Select(x => x.ToString()).Aggregate((f, s) => f + "," + s));
            return new Tuple<long, long>(fibonacci.Skip(1).FirstOrDefault(), fibonacci.FirstOrDefault());
        }
    }
}
