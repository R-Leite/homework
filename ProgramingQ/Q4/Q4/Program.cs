using System;
using System.Collections.Generic;
using System.Linq;

namespace Q4
{
    class Program
    {
        static void Main(string[] args)
        {
            var value1 = "send";
            var value2 = "more";
            var result = "money";

            Console.WriteLine("  " + value1);
            Console.WriteLine("+ " + value2);
            Console.WriteLine("------");
            Console.WriteLine(" " + result);

            var answerDictionary = SolveAlphametic(value1, value2, result);

            var val1Int = value1.Select(x => answerDictionary[x]).ToInt().ToString();
            var val2Int = value2.Select(x => answerDictionary[x]).ToInt().ToString();
            var resInt = result.Select(x => answerDictionary[x]).ToInt().ToString();

            Console.WriteLine();
            Console.WriteLine("  " + val1Int);
            Console.WriteLine("+ " + val2Int);
            Console.WriteLine("------");
            Console.WriteLine(" " + resInt);

            Console.WriteLine("終了するには何かキーを押してください。");
            Console.ReadKey();
        }

        static Dictionary<char, int> SolveAlphametic(string value1, string value2, string result)
        {
            var allString = new List<string>() { value1, value2, result };
            var firstChars = allString.Select(x=>x.First()).Distinct();
            var allChars = firstChars.Concat(allString.Aggregate((s, n) => s + n).ToString().Except(firstChars)).Distinct();
            var firstCharsNumber = firstChars.Count();

            // 最初の数字が0でないものと式が成立するものだけを順列から取り出す
            return Enumerable.Range(0, 10).Permutation(allChars.Count()).Where(x => !x.Take(firstCharsNumber).Contains(0))
                .Select(x => allChars.Zip(x, (k, v) => new { k, v }).ToDictionary(d => d.k, d => d.v))
                .Where(dict => result.Select(x => dict[x]).ToInt() == value1.Select(x => dict[x]).ToInt() + value2.Select(x => dict[x]).ToInt()).FirstOrDefault();
        }
    }

    // IEnumerable拡張メソッド
    static class IEnumerableExtension
    {
        // 順列の作成メソッド
        public static IEnumerable<IEnumerable<T>> Permutation<T>(this IEnumerable<T> list, int n)
        {
            if (n <= 0) return new List<List<T>>() { Enumerable.Empty<T>().ToList() };
            return list.SelectMany(head => list.Where(i => !head.Equals(i)).Permutation(n - 1).Select(tail => tail.Concat(Enumerable.Repeat(head, 1))));
        }

        // 整数リストから整数への変換メソッド
        public static int ToInt(this IEnumerable<int> list)
        {
            return list.Aggregate((a, b) => a * 10 + b);
        }
    }
}
