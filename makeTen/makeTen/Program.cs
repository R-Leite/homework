using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makeTen
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberList = Enumerable.Range(1, 9);
            var numberCombinationList = numberList.SelectMany(a =>
            numberList.SelectMany(b =>
            numberList.SelectMany(c =>
            numberList.Select(d => new NumberCombination(a, b, c, d)))));

            foreach(var i in numberCombinationList)
            {
                // 同じ数字があるやつはスキップ
//                if (i.ContainsSameNumber()) { continue; }

                if (i.isMakeTen())
                {
//                    Console.WriteLine(i.ToString());
                }
                else
                {
                    Console.WriteLine(i.ToString());
                }
            }

            Console.WriteLine("何かキーを押してください。");
            Console.ReadKey();
        }
    }
}
