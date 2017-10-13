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

            var list = new List<int>() { 0, 1, 2, 3 };

            var hoge = Permutation(3, list).ToList();
            Console.WriteLine(hoge);
            foreach(var i in hoge)
            {
                Console.WriteLine(i.Select(x => x.ToString()).Aggregate((a, b)=>a + "," + b));
            }
            Console.WriteLine("終了するには何かキーを押してください。");
            Console.ReadKey();
        }

        static void SolveAlphametic(string value1, string value2, string answer)
        {
            var allString = new List<string>() { value1, value2, answer };
            var firstChar = allString.Select(x=>x.First());
            var allChar = firstChar.Concat(allString.Aggregate((s, n) => s + n).ToString().Except(firstChar)).Distinct();

            Console.WriteLine(allChar.Aggregate("", (a, b) => a + b));
            Console.WriteLine(firstChar.Aggregate("", (a, b) => a + b));


        }

        static IEnumerable<IEnumerable<T>> Permutation<T>(int n, IEnumerable<T> list)
        {
            if (n <= 0) return new List<List<T>>() { Enumerable.Empty<T>().ToList() };
            return list.SelectMany(head => Permutation(n - 1, list.Where(i => !head.Equals(i))).Select(tail => tail.Concat(Enumerable.Repeat(head, 1))));
        }
    }
}
