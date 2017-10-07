using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    class Program
    {
        private const int MaxNumber = 10;

        static void Main(string[] args)
        {
            foreach(var i in GetXyCombination(MaxNumber))
            {
                Console.WriteLine($"x={i.Item1},y={i.Item2}");
            }
            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }

        static IEnumerable<Tuple<int, int>> GetXyCombination(int maxNumer)
        {
            return Enumerable.Range(1, maxNumer).SelectMany(_ => Enumerable.Range(1, _), (x, y) => new Tuple<int, int>(x, y));
        }
    }
}
