using System;
using System.Collections.Generic;
using System.Linq;

namespace Factoring
{
    class Program
    {
        static int count;

        static void Main(string[] args)
        {
            while (true)
            {
                // 標準入力
                Console.Write("Enter number (x ≧ 2) => ");
                int number;
                if (!int.TryParse(Console.ReadLine(), out number)) { Console.WriteLine("整数を入力して下さい。"); continue; }
                if (number < 2) { Console.WriteLine("2以上の整数を入力して下さい。"); continue; }

                // LINQ1行版
                count = 0;
                PrintAnswer(FactoringOneLinq(number), "LINQ1行版");

                // LINQ版
                count = 0;
                PrintAnswer(FactoringLinq(number), "LINQ版");

                // 通常再帰版
                count = 0;
                PrintAnswer(FactoringRecursionNormal(number, 2, new List<int>()), "通常再帰版");

                // 改良再帰版
                count = 0;
                PrintAnswer(FactoringRecursion(number, new List<int>()), "改良再帰版");


                // 通常
                //PrintAnswer(FactoringNormal(number), "通常版");
            }
        }

        static void PrintAnswer(IEnumerable<int> answer, string str)
        {
            Console.WriteLine(str);
            Console.WriteLine("呼び出し回数:" + count);
            Console.WriteLine(answer.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
            Console.Write(Environment.NewLine);
        }

        // LINQ1行版
        static IEnumerable<int> FactoringOneLinq(int number)
        {
            count++;

            return Enumerable.Range(2, number)
                .Where(x => number % x == 0)
                .Except(Enumerable.Range(2, number)
                    .Where(x => number % x == 0)
                    .SelectMany(x => Enumerable.Range(2, number)
                    .Where(y => number % y == 0), (a, b) => new { a, b })
                    .Where(y => y.a != y.b && y.a % y.b == 0)
                    .Select(y => y.a))
                .SelectMany(x => Enumerable.Repeat(x, Enumerable.Range(1, number).Where(y => number % Math.Pow(x, y) == 0).Last()));
        }

        // LINQ版
        static IEnumerable<int> FactoringLinq(int number)
        {
            count++;

            // ルート
            var sqrtNumber = (int)Math.Sqrt(number);

            // 素因数を作成
            var primeDivisor = Enumerable.Range(2, number).Where(x => number % x == 0).Where(x => Enumerable.Range(2, x).Where(y => x % y == 0).Count() <= 1);

            // 指数回分リストに格納
            var primeFactors = primeDivisor.SelectMany(x => Enumerable.Repeat(x, Enumerable.Range(1, sqrtNumber).Where(y => number % Math.Pow(x, y) == 0).Last()));

            return primeFactors;
        }


        // 通常再帰版
        static IEnumerable<int> FactoringRecursionNormal(int number, int factor, IEnumerable<int> factorList)
        {
            count++;

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
            count++;

            // 約数の最小値を取得
            var factor = Enumerable.Range(2, number).Where(x => number % x == 0).FirstOrDefault();

            // 母数が1以下なら素因数リストを返して終了
            if (number <= 1)
            {
                return factorList;
            }
            // 母数が最小約数以下なら母数を追加して素因数リストを返して終了
            if (number <= factor)
            {
                return factorList.Concat(Enumerable.Repeat(number, 1));
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
