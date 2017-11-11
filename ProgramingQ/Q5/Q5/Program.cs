using System;
using System.Collections.Generic;
using System.Linq;

namespace Q5
{
    class Program
    {
        static void Main(string[] args)
        {
            var floorDictionary = SolveFloor();
            foreach (var dict in floorDictionary.OrderByDescending(x => x.Value))
            {
                Console.WriteLine(dict);
            }

            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }

        static Dictionary<char, int> SolveFloor()
        {
            var floor = Enumerable.Range(1, 5);
            var floorDictionary =
                from A in floor
                from B in floor.Except(new[] { A })
                from C in floor.Except(new[] { A, B })
                from D in floor.Except(new[] { A, B, C })
                from E in floor.Except(new[] { A, B, C, D })
                where A != 5
                where B != 1
                where C != 5
                where C != 1
                where D > B
                where Math.Abs(E - C) != 1
                where Math.Abs(C - B) != 1
                select new Dictionary<char, int>() {
                       {'A', A },
                       {'B', B },
                       {'C', C },
                       {'D', D },
                       {'E', E }
                };
            return floorDictionary.FirstOrDefault();
        }
    }
}
