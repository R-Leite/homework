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
            GetMax(2);

            while (true) { }
        }

        static int GetMax(int digit)
        {
            var maxnum = (digit == 2) ? 99 : 999;
            if(digit==2)
            {

            }
            var aa = "abcde";
            Console.WriteLine(aa);
            string bb = aa.Reverse().ToString();
            Console.WriteLine(bb);
            var list = Enumerable.Range(10, 90).SelectMany(x => Enumerable.Range(10, 90).Select(y => x * y)).Where(x=>x.ToString()==x.ToString().Reverse().ToString());

            foreach(var i in list)
            {
                Console.WriteLine(i);
            }
            return 1;
        }
    }
}
