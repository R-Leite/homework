using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlphabetCount
{
    class Program
    {
        static string Pangram = "the quick brown fox jumps over the lazy dog";

        static void Main(string[] args)
        {
            CountLinq();

            CountNormal();

            while (true) { }
        }

        // LINQ
        static void CountLinq()
        {
            // 文章から空白を削除
            IEnumerable<char> s1 = Regex.Replace(Pangram, @"\s", "");
            Console.WriteLine("LINQ");
            Console.WriteLine(s1.OrderBy(x => x).Distinct().Select(x => x.ToString() + ":" + s1.Count(y => y == x)).Aggregate((a, b) => a + ", " + b));
        }

        // Normal
        static void CountNormal()
        {
            var dic = new Dictionary<char, int>();

            foreach(var i in Regex.Replace(Pangram, @"\s", ""))
            {
                if (dic.ContainsKey(i)) { dic[i]++; }
                else { dic.Add(i, 1); }
            }
            Console.WriteLine("Normal");
            Console.WriteLine(dic.OrderBy(x=>x.Key).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
        }
    }
}
