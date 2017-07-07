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
            PrintAnswer(CountLinq(Pangram), "LINQ");

            // Normal
            PrintAnswer(CountNormal(Pangram), "Normal");

            while (true) { }
        }

        // 表示
        static void PrintAnswer(Dictionary<char, int> dict, string str)
        {
            Console.WriteLine(str);
            Console.WriteLine(dict.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b) + Environment.NewLine);
        }

        // LINQ
        static Dictionary<char, int> CountLinq(string str)
        {
            return str.Where(x => x != ' ').OrderBy(x => x).GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }

        // Normal
        static Dictionary<char, int> CountNormal(string str)
        {
            var dic = new Dictionary<char, int>();

            foreach (var i in Regex.Replace(str, @"\s", ""))
            {
                if (dic.ContainsKey(i)) { dic[i]++; }
                else { dic.Add(i, 1); }
            }

            return dic;
        }
    }
}
