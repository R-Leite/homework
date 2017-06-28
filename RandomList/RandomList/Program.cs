using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomList
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1行
            PrintAnswer(Linq(), "1行");

            // yield
            PrintAnswer(Yield(), "yield");

            // 再帰
            PrintAnswer(Recursive(), "再帰");

            // 通常
            PrintAnswer(Normal(), "通常");

            while (true) { }
        }

        static void PrintAnswer(IEnumerable<int> list, string str)
        {
            Console.WriteLine(str);
            if (list.Count() < 1) { Console.WriteLine("リストなし"); }
            else { Console.WriteLine(list.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b)); }
            Console.WriteLine();
        }

        // 1行版
        static IEnumerable<int> Linq()
        {
            var random = new Random();
            return Enumerable.r().Next(10), new Random(3).Next(10)).Select(x => x);
        }

        // yield
        static IEnumerable<int> Yield()
        {
            return YieldLoop().TakeWhile(x => x != 9);
        }

        static IEnumerable<int> YieldLoop()
        {
            var random = new Random();
            　
            while (true)
            {
                yield return random.Next(10);
            }
        }

        // 再帰
        static IEnumerable<int> Recursive()
        {
            var random = new Random();
            return RecursiveLoop(new List<int>(), random);
        }

        static IEnumerable<int> RecursiveLoop(IEnumerable<int> rlist, Random random)
        {
            var value = random.Next(10);
            if (value == 9) { return rlist; }

            return RecursiveLoop(rlist.Concat(Enumerable.Repeat(value, 1)), random);
        }

        // immutableじゃない通常
        static IEnumerable<int> Normal()
        {
            var list = new List<int>();
            var random = new Random();

            while(true)
            {
                var value = random.Next(10);
                if (value == 9) { break; }
                list.Add(value);
            }

            return list;
        }
    }
}
