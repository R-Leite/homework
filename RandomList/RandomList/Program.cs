﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomList
{
    class Program
    {
        const int Range = 10;
        const int EndValue = 9;

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
            try
            {
                Console.WriteLine(str);
                if (list.Count() < 1) { Console.WriteLine("最初に9が取得されました。"); }
                else { Console.WriteLine(list.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b)); }
                Console.WriteLine();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(list.Count());
            }
        }

        // 1行版
        static IEnumerable<int> Linq()
        {
            return Enumerable.Repeat(new Random(), int.MaxValue).Select(x => x.Next(Range)).TakeWhile(x => x != EndValue);
        }

        // yield
        static IEnumerable<int> Yield()
        {
            return YieldLoop().TakeWhile(x => x != EndValue);
        }

        static IEnumerable<int> YieldLoop()
        {
            var random = new Random();
            　
            while (true)
            {
                yield return random.Next(Range);
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
            var value = random.Next(Range);
            return (value == EndValue) ? rlist : RecursiveLoop(rlist.Concat(Enumerable.Repeat(value, 1)), random);
        }

        // immutableじゃない通常
        static IEnumerable<int> Normal()
        {
            var list = new List<int>();
            var random = new Random();

            while(true)
            {
                var value = random.Next(Range);
                if (value == EndValue) { break; }
                list.Add(value);
            }

            return list;
        }
    }
}
