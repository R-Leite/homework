using System;
using System.Collections.Generic;

namespace ArithmeticOperation
{
    class Program
    {
        // 計算処理の辞書を作成
        private static Dictionary<string, Func<double, double, double>> _calcDict = new Dictionary<string, Func<double, double, double>>
        {
            {"+", (x, y) => x + y},
            {"-", (x, y) => x - y},
            {"*", (x, y) => x * y},
            {"/", (x, y) => x / y}
        };

        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');
            var op = input[1];
            var val1 = double.Parse(input[0]);
            var val2 = double.Parse(input[2]);
            Console.WriteLine(val1 + " " + op + " " + val2 + " = " + Calculate(op, val1, val2));

            Console.ReadLine();
        }

        // 四則演算の計算結果を返す
        static double Calculate(string operater, double a, double b)
        {
            return _calcDict[operater](a, b);
        }
    }
}
