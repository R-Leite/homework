using System;
using System.Collections.Generic;
using System.Linq;

namespace NewEmployeePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Q3
            Console.WriteLine("3. 与えられた正数値の階乗を返す関数を作りなさい。");
            Console.Write("正数値：");
            if (int.TryParse(Console.ReadLine(), out var input))
            {
                if (input > 0)
                {
                    Console.WriteLine($"{input}の階乗：{Q3(input)}");
                }
            }
            Console.WriteLine();
            
            // Q5
            Console.WriteLine("5. 与えられた自然数数値列から3による剰余が0である最初の数値を返す関数を作りなさい。ただし、該当する数値が見つからない場合は0を返すものとします。");
            Console.Write("自然数数値列(スペース区切り)：");
            var inputCollection = Console.ReadLine().Split(' ').Where(x => int.TryParse(x, out int _)).Select(x => int.Parse(x));
            Console.WriteLine(Q5(inputCollection));
            Console.WriteLine();

            // Q6
            Console.WriteLine("6. 与えられた正数数値列の要素の重複を無くし、昇順に並び替えて返す関数を作りなさい。");
            Console.Write("自然数数値列(スペース区切り)：");
            inputCollection = Console.ReadLine().Split(' ').Where(x => int.TryParse(x, out int _)).Select(x => int.Parse(x));
            Console.WriteLine(Q6(inputCollection).ToStinrg());
            Console.WriteLine();

            // Q8
            Console.WriteLine("8. 与えられた2次元正数数値列をフラットな1次元正数数値列に変換して返す関数を作りなさい。");
            var inputDoubleCollection = new List<List<int>>() { new List<int>() { 1, 2, 3, 4, 5 }, new List<int>() { 11, 12, 13, 14, 15 } };
            Console.WriteLine($"2次元正数数値列：{inputDoubleCollection.Select(x => x.ToStinrg()).ToStinrg()}");
            Console.WriteLine(Q8(inputDoubleCollection).ToStinrg());
            Console.WriteLine();

            // Q9
            Console.WriteLine("9. 与えられた2つの文字列コレクションから結合・加工したひとつの文字列コレクションを返す関数を作りなさい。2つの文字列は同じ要素番号を持つ者同士をコロンを用いて結合し、結合可能なもののみを返すこと。");
            var stringList1 = new List<String>() { "apple", "orange", "banana" };
            var stringList2 = new List<String>() { "japan", "america", "brazil", "china" };
            Console.WriteLine($"文字列コレクション1：{stringList1.ToStinrg()}");
            Console.WriteLine($"文字列コレクション2：{stringList2.ToStinrg()}");
            Console.WriteLine(Q9(stringList1, stringList2).ToStinrg());
            Console.WriteLine();

            // Q10
            Console.WriteLine("10. 指定された個数の2桁の自然数を乱数で生成し、数値列として返す関数を作りなさい。");
            Console.Write("正数値：");
            if (int.TryParse(Console.ReadLine(), out input))
            {
                if (input > 0)
                {
                    Console.WriteLine($"{input}の2桁ランダム値：{Q10(input).ToStinrg()}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("続行するには何かキーを押してください。");
            Console.ReadKey();

        }

        /// <summary>
        /// 3. 与えられた正数値の階乗を返す関数を作りなさい。
        /// </summary>
        /// <param name="_num"></param>
        /// <returns></returns>
        static ulong Q3(int _num)
        {
            return Enumerable.Range(1, _num).Select(x => (ulong)x).Aggregate((a, b) => a * b);
        }

        /// <summary>
        /// 5. 与えられた自然数数値列から3による剰余が0である最初の数値を返す関数を作りなさい。ただし、該当する数値が見つからない場合は0を返すものとします。
        /// </summary>
        /// <param name="_collection"></param>
        /// <returns></returns>
        static int Q5(IEnumerable<int> _collection)
        {
            return (int?)_collection.FirstOrDefault(x => x % 3 == 0) ?? 0;
        }

        /// <summary>
        /// 6. 与えられた正数数値列の要素の重複を無くし、昇順に並び替えて返す関数を作りなさい。
        /// </summary>
        /// <param name="_collection"></param>
        /// <returns></returns>
        static IEnumerable<int> Q6(IEnumerable<int> _collection)
        {
            return _collection.Distinct().OrderBy(x => x);
        }

        /// <summary>
        /// 8. 与えられた2次元正数数値列をフラットな1次元正数数値列に変換して返す関数を作りなさい。
        /// </summary>
        /// <param name="_collection"></param>
        /// <returns></returns>
        static IEnumerable<int> Q8(IEnumerable<IEnumerable<int>> _collection)
        {
            return _collection.SelectMany(x => x);
        }

        /// <summary>
        /// 9. 与えられた2つの文字列コレクションから結合・加工したひとつの文字列コレクションを返す関数を作りなさい。2つの文字列は同じ要素番号を持つ者同士をコロンを用いて結合し、結合可能なもののみを返すこと。
        /// </summary>
        /// <param name="_stringList1"></param>
        /// <param name="_stringList2"></param>
        /// <returns></returns>
        static IEnumerable<string> Q9(IEnumerable<string> _stringList1, IEnumerable<string> _stringList2)
        {
            return _stringList1.Zip(_stringList2, (a, b) => $"{a}:{b}");
        }

        /// <summary>
        /// 指定された個数の 2 桁の自然数を乱数で生成し、数値列として返す関数を作りなさい。
        /// </summary>
        /// <returns></returns>
        static IEnumerable<int> Q10(int _number)
        {
            return Enumerable.Repeat(new Random(), _number).Select(x => x.Next(10, 100));
        }

    }

    public static class ExtensionClass
    {
        /// <summary>
        /// IEnumrableの文字列を返す拡張メソッド
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_collection"></param>
        /// <returns></returns>
        public static string ToStinrg<T>(this IEnumerable<T> _collection)
        {
            return $"[{_collection.Select(x => x.ToString()).Aggregate((a, b) => $"{a}, {b}")}]";
        }

    }
}
