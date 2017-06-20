using System;
using System.Collections.Generic;
using System.Linq;

namespace Factoring
{
    class Program
    {
        static int recursionCount;

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


                // 再帰版
                recursionCount = 0;
                var divisor = MakeDivisor(number);
                Console.WriteLine(divisor.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                Console.WriteLine("再帰版");
                Console.WriteLine(FactoringRecursion(number, divisor.FirstOrDefault(), new List<int>(), divisor).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
                Console.WriteLine("呼び出し回数:" + recursionCount);


                // 通常
                Console.WriteLine("通常版");
                Console.WriteLine(FactoringNormal(number).Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
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

        // 再帰版
        static IEnumerable<int> FactoringRecursion(int number, int factor, IEnumerable<int> factorList, IEnumerable<int> divisorList)
        {
            recursionCount++;

            if (number < factor * factor)
            {
                return number != 1 ? factorList.Concat(Enumerable.Repeat(number, 1)) : factorList;
            }
            if(number % factor == 0)
            {
                return FactoringRecursion(number / factor, factor, factorList.Concat(Enumerable.Repeat(factor, 1)), divisorList);
            }
            else
            {
                return FactoringRecursion(number, divisorList.Skip(1).FirstOrDefault(), factorList, divisorList.Skip(1));
            }
        }

        // 素因数リストを作成
        static IEnumerable<int> MakeDivisor(int number)
        {
            recursionCount++;

            return Enumerable.Range(2, number).Where(x => number % x == 0).Where(x => Enumerable.Range(2, x).Where(y => x % y == 0).Count() <= 1);
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
