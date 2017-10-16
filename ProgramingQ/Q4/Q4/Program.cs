using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4
{
    class Program
    {
        static void Main(string[] args)
        {
            var value1 = "send";
            var value2 = "more";
            var answer = "money";

            SolveAlphametic(value1, value2, answer);

            Console.WriteLine("終了するには何かキーを押してください。");
            Console.ReadKey();
        }

        static void SolveAlphametic(string value1, string value2, string answer)
        {
            var allString = new List<string>() { value1, value2, answer };
            var firstChars = allString.Select(x=>x.First()).Distinct();
            var allChars = firstChars.Concat(allString.Aggregate((s, n) => s + n).ToString().Except(firstChars)).Distinct();
            var firstCharsNumber = firstChars.Count();

            var aaa = Enumerable.Range(0, 10).Permutation(allChars.Count()).Where(x => x.Select((y, i) => (i < firstCharsNumber & y == 0)).Aggregate((a, b) => a || b))
                .Select(x => allChars.Zip(x, (k, v) => new { k, v }).ToDictionary(a => a.k, a => a.v))
                .Where(x => answer.Select(y => x[y]).Aggregate((a, b) => a * 10 + b) == value1.Select(y => x[y]).Aggregate((a, b) => a * 10 + b) + value2.Select(y => x[y]).Aggregate((a, b) => a * 10 + b));

            foreach(var i in aaa) { Console.WriteLine(i); }
            //foreach (var i in Enumerable.Range(0, 10).Permutation(allChars.Count()))
            //{
            //    if (Math.Abs(i.ToList().IndexOf(0)) < firstCharsNumber) { continue; }
            //    var hoge = allChars.Zip(i, (k, v) => new { k, v }).ToDictionary(a => a.k, a => a.v);
            //    if (answer.Select(x => hoge[x]).Aggregate((a, b) => a * 10 + b) == value1.Select(x => hoge[x]).Aggregate((a, b) => a * 10 + b) + value2.Select(x => hoge[x]).Aggregate((a, b) => a * 10 + b))
            //    {
            //        foreach (var j in hoge)
            //        {
            //            Console.WriteLine(j);
            //        }
            //    }
            //}


        }
    }
    static class IEnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> Permutation<T>(this IEnumerable<T> list, int n)
        {
            if (n <= 0) return new List<List<T>>() { Enumerable.Empty<T>().ToList() };
            return list.SelectMany(head => list.Where(i => !head.Equals(i)).Permutation(n - 1).Select(tail => tail.Concat(Enumerable.Repeat(head, 1))));
        }
    }
}
