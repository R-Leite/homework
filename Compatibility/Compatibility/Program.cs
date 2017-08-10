using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compatibility
{
    class Program
    {
        static string _space = "";

        static void Main(string[] args)
        {
            var first = new List<int>() { 6, 8, 7, 7 };
            var second = new List<int>() { 5, 7, 9, 4, 5 };

            Console.WriteLine("相性:" + int.Parse(FortuneTelling(first.Concat(second)).Select(x => x.ToString()).Aggregate((a, b) => a + b)) + "%");

            while (true) { }
        }

        // 相性を計算する関数
        static IEnumerable<int> FortuneTelling(IEnumerable<int> StrokeCount)
        {
            Console.Write(_space += " ");
            Console.WriteLine(StrokeCount.Select(x => x.ToString()).Aggregate((a, b) => a + " " + b));

            if (int.Parse(StrokeCount.Select(x => x.ToString()).Aggregate((a, b) => a + b)) <= 100) { return StrokeCount; }
            return FortuneTelling(StrokeCount.Zip(StrokeCount.Skip(1), Tuple.Create).Select(x => (x.Item1 + x.Item2) % 10));
        }
    }
}
