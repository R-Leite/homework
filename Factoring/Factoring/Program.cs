using System;
using System.Collections.Generic;
using System.Linq;

namespace Factoring
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // 標準入力
                Console.Write("Enter number (x ≧ 2) => ");
                int number;
                if (!int.TryParse(Console.ReadLine(), out number)) { Console.WriteLine("整数を入力して下さい。"); continue; }

                // 入力数字のルート
                var sqrt = (int)Math.Ceiling(Math.Sqrt(number));

                // 入力数字の約数かつ素数を求める
                var primeDivisor = Enumerable.Range(2, number).Where(x => Enumerable.Range(2, x).Where(y => x % y == 0).Count() <= 1).Where(x => number % x == 0).ToList();

                // それぞれの素約数の指数を求め、その回数分並べる
                var primeFactors = primeDivisor.SelectMany(x => Enumerable.Repeat(x, Enumerable.Range(1, sqrt).Where(y => number % Math.Pow(x, y) == 0).Max()));

                // 表示
                Console.WriteLine(primeFactors.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));

                // 通常
                Console.WriteLine("通常方法での答え");
                Console.WriteLine(FactoringNormal(number).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
            }
        }

        // 通常方法
        static IEnumerable<int> FactoringNormal(int number)
        {
            while (true)
            {
                var i = 2;
                while (i < number)
                {
                    if (number % i == 0)
                    {
                        number /= i;
                        yield return i;
                        break;
                    }
                    i++;
                }
                if (i == number) { yield return i; break; }
            }
        }
    }
}
