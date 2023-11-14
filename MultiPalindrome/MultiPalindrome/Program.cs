using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw;
            var digits = new List<int>() { 2, 3 };

            foreach (var digit in digits)
            {
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                Console.WriteLine(GetMax(digit));
                sw.Stop();
                Console.WriteLine(sw.Elapsed);
            }

            foreach(var digit in digits)
            { 
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                Console.WriteLine(GetOrderByDescendingFirst(digit));
                sw.Stop();
                Console.WriteLine(sw.Elapsed);
            }

            while (true) { }
        }

        static void hoge(object aaa)
        {
            Console.WriteLine(aaa);
        }

        static int GetOrderByDescendingFirst(int digit)
        {
            var numList = (digit == 2) ? Enumerable.Range(10, 90) : Enumerable.Range(100, 900);

            return numList.SelectMany(x => numList.Select(y => x * y)).OrderByDescending(x => x).Where(x => x.ToString() == x.ToString().Reverse().Aggregate("", (a, b) => a + b)).FirstOrDefault();
        }

        static int GetMax(int digit)
        {
            var numList = (digit == 2) ? Enumerable.Range(10, 90) : Enumerable.Range(100, 900);

            return numList.SelectMany(x => numList.Select(y => x * y)).Where(x => x.ToString() == x.ToString().Reverse().Aggregate("", (a, b) => a + b)).Max();
        }
    }
}
