using System;
using System.Collections.Generic;
using System.Linq;

namespace NewEmployeePractice10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("表示する個数を入力して下さい。：");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input > 0)
                {
                    Console.WriteLine(GetRandomNaturalNumber(input).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                }
            }
            else
            {
                Console.WriteLine("数字を入力して下さい。");
            }

            Console.WriteLine("続行するには何かキーを押して下さい。");
            Console.ReadKey();
        }
        /// <summary>
        /// 指定された個数の 2 桁の自然数を乱数で生成し、数値列として返す関数を作りなさい。
        /// </summary>
        /// <returns></returns>
        static IEnumerable<int> GetRandomNaturalNumber(int _number)
        {
            return Enumerable.Repeat(new Random(), _number).Select(x => x.Next(10, 99));
        }
    }
}
