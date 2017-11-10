using System;
using System.Collections.Generic;
using System.Linq;

namespace Q2
{
    class Program
    {
        private const int MaxNumber = 10;

        static void Main(string[] args)
        {
            foreach(var i in GetXyCombination(MaxNumber))
            {
                Console.WriteLine($"{i}");
            }
            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }

        static IEnumerable<object> GetXyCombination(int maxNumer)
        {
            return Enumerable.Range(1, maxNumer).SelectMany(_ => Enumerable.Range(1, _), (x, y) => new { x, y });
        }
    }
}
