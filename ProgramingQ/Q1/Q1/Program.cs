using System;
using System.Linq;

namespace Q1
{
    class Program
    {
        const int CUTCOUNT = 40;

        static void Main(string[] args)
        {
            var lines = GetVerticalHorizontalLines(CUTCOUNT);
            Console.WriteLine("カット回数：" + CUTCOUNT);
            Console.WriteLine("縦の長さ：" + lines.Item1);
            Console.WriteLine("横の長さ：" + lines.Item2);
            Console.ReadKey();
        }

        // 指定カット回数時の縦横の長さを返す
        static Tuple<long, long> GetVerticalHorizontalLines(int cutCount)
        {
            // 縦横の長さはフィボナッチ数列的に増えていくので2始まりのフィボナッチ数列を作成
            var fibonacci = Enumerable.Repeat(new[] { 2L, 1L }, cutCount + 1).Select(x => x[1] = (x[0] = x[0] + x[1]) - x[1]).Reverse().ToList();
            return new Tuple<long, long>(fibonacci.Skip(1).FirstOrDefault(), fibonacci.FirstOrDefault());
        }
    }
}
