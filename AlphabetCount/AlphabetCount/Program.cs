using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlphabetCount
{
    class Program
    {
        static void Main(string[] args)
        {
            string Pangram = "the quick brown fox jumps over the lazy dog";

            // Linq
            CountLinq(Pangram);

            // Normal
            CountNormal(Pangram);

            while (true) { }
        }

        // LINQ
        static void CountLinq(string str)
        {
            var s1 = str.Where(x => x != ' ').GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            Console.WriteLine("LINQ");
            Console.WriteLine(s1.OrderBy(x => x.Key).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
        }

        // Normal
        static void CountNormal(string str)
        {
            var dic = new Dictionary<char, int>();

            foreach (var i in Regex.Replace(str, @"\s", ""))
            {
                if (dic.ContainsKey(i)) { dic[i]++; }
                else { dic.Add(i, 1); }
            }

            Console.WriteLine("Normal");
            Console.WriteLine(dic.OrderBy(x => x.Key).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
        }
    }
}
