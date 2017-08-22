using System;
using System.Collections.Generic;
using System.Linq;

namespace Compatibility
{
    class Program
    {
        static string _space = "";

        static void Main(string[] args)
        {
            var first = "5,1,4,5";
            var second = "1,6,9,6";

            var strokeConcat = first.Split(',').Concat(second.Split(',')).Select(x => int.Parse(x));

            Console.WriteLine(Environment.NewLine + "相性:" + int.Parse(FortuneTelling(strokeConcat).Select(x => x.ToString()).Aggregate((a, b) => a + b)) + "%");

            while (true) { }
        }

        // 相性を計算する関数
        static IEnumerable<int> FortuneTelling(IEnumerable<int> StrokeCount)
        {
            //Console.Write(_space += " ");
            //Console.WriteLine(StrokeCount.Select(x => x.ToString()).Aggregate((a, b) => a + " " + b));
            if (long.Parse(StrokeCount.Select(x => x.ToString()).Aggregate((a, b) => a + b)) <= 100) { return StrokeCount; }
            return FortuneTelling(StrokeCount.Zip(StrokeCount.Skip(1), Tuple.Create).Select(x => (x.Item1 + x.Item2) % 10));
        }
    }
}
