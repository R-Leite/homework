using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compatibility
{
    class Program
    {
        static void Main(string[] args)
        {
            var first = new List<int>() { 6, 8, 7, 7 };
            var second = new List<int>() { 5, 7, 9, 4, 5 };

            var sa = FortuneTelling(first.Concat(second));

            foreach(var i in sa)
            {
                Console.Write(i);
            }

            while (true) { }
        }

        static IEnumerable<int> FortuneTelling(IEnumerable<int> StrokeCount)
        {
            foreach(var i in StrokeCount) { Console.Write(i + " "); }
            Console.WriteLine();
            if (int.Parse(StrokeCount.Select(x => x.ToString()).Aggregate((a, b) => a + b)) <= 100) { return StrokeCount; }
            return FortuneTelling(StrokeCount.Zip(StrokeCount.Skip(1), Tuple.Create).Select(x => (x.Item1 + x.Item2) % 10));
        }
    }
}
