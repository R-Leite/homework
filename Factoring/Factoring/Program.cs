using System;
using System.Collections.Generic;
using System.Linq;

namespace Factoring
{
    class Program
    {
        static int improveCount;
        static int normalCount;

        static void Main(string[] args)
        {
            while (true)
            {
                // 標準入力
                Console.Write("Enter number (x ≧ 2) => ");
                int number;
                if (!int.TryParse(Console.ReadLine(), out number)) { Console.WriteLine("整数を入力して下さい。"); continue; }
                if (number < 2) { Console.WriteLine("2以上の整数を入力して下さい。"); continue; }

                // LINQ版
                Console.WriteLine("LINQ版");
                Console.WriteLine(FactoringLinq(number).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                Console.Write(Environment.NewLine);

                // 通常再帰版
                normalCount = 0;
                Console.WriteLine("通常再帰");
                Console.WriteLine(FactoringRecursionNormal(number, 2, new List<int>()).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                Console.WriteLine("呼び出し回数:" + normalCount);
                Console.Write(Environment.NewLine);

                // 再帰版
                improveCount = 0;
                Console.WriteLine("改善再帰版");
                Console.WriteLine(FactoringRecursion(number, new List<int>()).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                Console.WriteLine("呼び出し回数:" + improveCount);
                Console.Write(Environment.NewLine);

                // 通常
                Console.WriteLine("通常版");
                Console.WriteLine(FactoringNormal(number).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                Console.Write(Environment.NewLine);
            }
        }

        // LINQ版
        static IEnumerable<int> FactoringLinq(int number)
        {
            // ルート
            var sqrtNumber = (int)Math.Ceiling(Math.Sqrt(number));

            // 入力数字の約数かつ素数を求める
            var primeDivisor = Enumerable.Range(2, number).Where(x => number % x == 0).Where(x => Enumerable.Range(2, x).Where(y => x % y == 0).Count() <= 1);

            // それぞれの素約数の指数を求め、その回数分並べる
            var primeFactors = primeDivisor.SelectMany(x => Enumerable.Repeat(x, Enumerable.Range(1, sqrtNumber).Where(y => number % Math.Pow(x, y) == 0).Max()));

            return primeFactors;
        }

        // 通常再帰版
        static IEnumerable<int> FactoringRecursionNormal(int number, int factor, IEnumerable<int> factorList)
        {
            normalCount++;

            if (number < factor * factor)
            {
                return number != 1 ? factorList.Concat(Enumerable.Repeat(number, 1)) : factorList;
            }
            if (number % factor == 0)
            {
                return FactoringRecursionNormal(number / factor, factor, factorList.Concat(Enumerable.Repeat(factor, 1)));
            }
            else
            {
                return FactoringRecursionNormal(number, factor + 1, factorList);
            }
        }


        // 改善再帰版
        static IEnumerable<int> FactoringRecursion(int number, IEnumerable<int> factorList)
        {
            improveCount++;

            // 約数の最小値を取得
            var factor = Enumerable.Range(2, number).Where(x => number % x == 0).FirstOrDefault();

            // 母数が2未満なら素因数リストを返して終了
            if (factor <= 0)
            {
                return factorList;
            }

            // 母数が最小約数以下なら素因数リストを返して終了
            if (number <= factor)
            {
                return number != 1 ? factorList.Concat(Enumerable.Repeat(number, 1)) : factorList;
            }
            if(number % factor == 0)
            {
                var index = Enumerable.Range(1, number).Where(x => number % Math.Pow(factor, x) == 0).Last();
                return FactoringRecursion(number / (int)Math.Pow(factor, index), factorList.Concat(Enumerable.Repeat(factor, index)));
            }
            // 入らない
            else
            {
                return FactoringRecursion(number, factorList);
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
