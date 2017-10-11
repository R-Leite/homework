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
            //for (var s = 0; s < 10; s++)
            //{
            //    for (var e = s; e < 10; e++)
            //    {
            //        for (var n = e; n < 10; n++)
            //        {
            //            for (var d = n; d < 10; d++)
            //            {
            //                for (var m = d; m < 10; m++)
            //                {
            //                    for (var o = 0; o < 10; o++)
            //                    {
            //                        for (var r = 0; r < 10; r++)
            //                        {
            //                            for (var y = 0; y < 10; y++)
            //                            {
            //                                var send = s * 1000 + e * 100 + n * 10 + d;
            //                                var more = m * 1000 + o * 100 + r * 10 + e;
            //                                var money = m * 10000 + o * 1000 + n * 100 + e * 10 + y;
            //                                if (s == 0) { continue; }
            //                                if (m == 0) { continue; }
            //                                if (s == e || s == n || s == d || s == m || s == o || s == r || s == y) { continue; }
            //                                if (e == n || e == d || e == m || e == o || e == r || e == y) { continue; }
            //                                if (n == d || n == m || n == o || n == r || n == y) { continue; }
            //                                if (d == m || d == o || d == r || d == y) { continue; }
            //                                if (m == o || m == r || m == y) { continue; }
            //                                if (o == r || o == y) { continue; }
            //                                if (r == y) { continue; }
            //                                if (send + more == money)
            //                                {
            //                                    Console.WriteLine($"{s},{e},{n},{d},{m},{o},{n},{e},{y}");
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            var list = new List<int>() { 0, 1, 2, 3};
            var ans = new List<int>();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                { 
                    for (var k = 0; k < 3; k++)
                    {
                        if (i == j) continue;
                        if (i == k) continue;
                        if (j == k) continue;
                        Console.WriteLine($"{i}{j}{k}");
                    }
                }
            }
            permu(list, ans, 3);
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

        static IEnumerable<int> permu(IEnumerable<int> alr, IEnumerable<int> ans, int n)
        {
            if(alr.Count() == 1) { return alr; }
            foreach(var i in alr)
            {
                ans.Concat(Enumerable.Repeat(i, 1));
                if (alr.Count() == 1) { Console.WriteLine(i); }
                else { Console.Write(i); }
                permu(alr.Except(Enumerable.Repeat(i, 1)), ans, n-1);
            }
            return alr;
        }
    }
}
