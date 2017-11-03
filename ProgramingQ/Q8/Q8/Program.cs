using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Q8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
//                var fileName = @"Q8_small_in.txt";
                var fileName = @"Q8_large_in.txt";
                var file = File.ReadLines(fileName);

                var testCaseNumber = int.Parse(file.FirstOrDefault());
                foreach (var ans in file.Skip(1).Take(testCaseNumber).Select(x => ConvertNumeration(x)).Select((str, idx)=> new { str, idx }))
                {
                    Console.WriteLine($"Case #{(ans.idx+1).ToString().PadLeft(3, '0')}: {ans.str}");
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("終了するには何かキーを押してください...");
                Console.ReadKey();
            }
        }

        static string ConvertNumeration(string input)
        {
            var testCase = input.Split(' ');

            // 記数法のリスト作成
            var beforeList = testCase.Skip(1).FirstOrDefault();
            var afterList = testCase.Skip(2).FirstOrDefault();

            // 入力文字を10進数に変換
            var decimalNumber = testCase.First().Select(x => beforeList.IndexOf(x)).Aggregate((a, b) => a * beforeList.Count() + b);

            // 10進数から与えられた記数法に変換
#if false
            var specifyNumber = decimalNumber.ConvertTo(afterList.Count(), new List<int>()).Select(x => afterList[x]);
#else
            var specifyNumber = decimalNumber.ConvertTo(afterList.Count()).Select(x => afterList[x]);
#endif
            return specifyNumber.Aggregate("", (a, b) => a + b);
        }
    }

    public static class intExtensions
    {
        // 10進数⇒n進数
        // 再帰版
        public static IEnumerable<int> ConvertTo(this int number, int dec, IEnumerable<int> convertList)
        {
            if (number < dec) return convertList.Concat(Enumerable.Repeat(number, 1)).Reverse();
            return (number / dec).ConvertTo(dec, convertList.Concat(Enumerable.Repeat((number % dec), 1)));
        }

        // LINQ版
        public static IEnumerable<int> ConvertTo(this int number, int dec)
        {
            var list = Enumerable.Range(0, (int)Math.Log(number, dec) + 1).Select(x => (int)Math.Pow(dec, x));
            return list.Select(x => (int)(list.Reverse().TakeWhile(y => y > x).Aggregate(number, (a, b) => (a < b) ? a : a % b) / x)).Reverse();
        }
    }
}
