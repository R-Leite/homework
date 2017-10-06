using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            var hoge = Enumerable.Range(1, 10).SelectMany(x => Enumerable.Range(1, x)).Select(x=>x.ToString()).Aggregate((a, b) => a + "," + b);
            Console.WriteLine(hoge);
            Console.ReadKey();
        }
    }
}
