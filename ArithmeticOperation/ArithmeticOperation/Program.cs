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
            var op = "+";
            var val1 = 5;
            var val2 = 7;
            Console.WriteLine(val1 + " " + op + " " + val2 + " = " + Calculate(op, val1, val2));

            op = "-";
            val1 = 2;
            val2 = 8;
            Console.WriteLine(val1 + " " + op + " " + val2 + " = " + Calculate(op, val1, val2));

            op = "*";
            val1 = 7;
            val2 = 15;
            Console.WriteLine(val1 + " " + op + " " + val2 + " = " + Calculate(op, val1, val2));

            op = "/";
            val1 = 12;
            val2 = 5;
            Console.WriteLine(val1 + " " + op + " " + val2 + " = " + Calculate(op, val1, val2));

            while (true) { }
        }

        // 四則演算の計算結果を返す
        static double Calculate(string operater, double a, double b)
        {
           return _calcDict[operater](a, b);
        }
    }
}
