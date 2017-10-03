using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 40;
            var hoge = Fibonaci(n);
            Console.WriteLine(hoge);

            Console.ReadKey();
        }

        static Tuple<long, long> Fibonaci(int n)
        {
            var fib = Enumerable.Repeat(new[] { 2L, 1L }, n).Select(x => x[1] = (x[0] = x[0] + x[1]) - x[1]).Reverse();
            Console.WriteLine(fib.Select(x => x.ToString()).Aggregate((a, b) => a + "," + b));
            return new Tuple<long, long>(fib.FirstOrDefault(), fib.Skip(1).FirstOrDefault());
        }
    }
}
