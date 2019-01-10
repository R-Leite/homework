using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitUse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("空白区切りで数値を入れて下さい。");
            var input = Console.ReadLine().Split(' ');
            var value1 = int.Parse(input[0]);
            var value2 = int.Parse(input[1]);

            Console.WriteLine($"入力値 -> value1={value1}  value2={value2}");

            //(value1, value2) = (value2, value1);
            // XORで入れ替え
            value1 ^= value2;
            value2 ^= value1;
            value1 ^= value2;
            Console.WriteLine($"入替後 -> value1={value1}  value2={value2}");
            Console.WriteLine();

            var max = Max1(value1, value2);
            Console.WriteLine($"大きい値 -> {max}");
            max = Max2(value1, value2);
            Console.WriteLine($"大きい値 -> {max}");
            Console.WriteLine("続行するには何かキーを押してください。");
            Console.ReadKey();
        }

        #region 大きいほうを取得する関数
        static public int Max1(int value1, int value2)
        {
            return new List<int> { value1, value2 }.OrderByDescending(x => x).First();
        }

        static public int Max2(int value1, int value2)
        {
            return ((value1 + value2) + Math.Abs(value1 - value2)) / 2;
        }

        //static public int Max3(int value1, int value2)
        //{
        //    return 1;
        //}
        #endregion
    }
}
