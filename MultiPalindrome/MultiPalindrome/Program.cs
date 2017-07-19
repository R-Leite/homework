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
            var sw = new System.Diagnostics.Stopwatch();

            sw.Start();
            GetMax(2);
            sw.Stop();

            Console.WriteLine(sw.Elapsed);


            while (true) { }
        }

        static int GetMax(int digit)
        {
            var maxnum = (digit == 2) ? 99 : 999;
            if(digit==2)
            {

            }
            var list = Enumerable.Range(10, 90).SelectMany(x => Enumerable.Range(10, 90).Select(y => x * y)).Where(x => x.ToString() == String.Join("", x.ToString().Reverse())).Max();

            foreach(var i in list)
            {
                Console.WriteLine(i);
            }
            return 1;
        }
    }
}
