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

            // それぞれの画数をカンマで区切って連結
            var strokeConcat = first.Split(',').Concat(second.Split(',')).Select(x => int.Parse(x));

            Console.WriteLine(Environment.NewLine + "相性:" + FortuneTelling(strokeConcat) + "%");

            // 入力があったら終了
            Console.ReadLine();
        }

        // 相性を計算する再帰関数
        static int FortuneTelling(IEnumerable<int> StrokeCount)
        {
            Console.Write(_space += " ");
            Console.WriteLine(StrokeCount.Select(x => x.ToString()).Aggregate((a, b) => a + " " + b));

            if (StrokeCount.Count() < 4)
            {
                var compatible = StrokeCount.Aggregate((x, y) => x * 10 + y);
                if (compatible <= 100) { return compatible; }
            }
            return FortuneTelling(StrokeCount.Zip(StrokeCount.Skip(1), (first, second) => (first + second) % 10));
        }
    }
}
