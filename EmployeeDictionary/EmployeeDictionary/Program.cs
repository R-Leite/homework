using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeData = new List<string>() { "19336", "青木秀樹", "19337", "岩本康宏", "69210", "メーカインウー", "69281", "宇賀勇太" };

            PrintAnswer(ZipOneLiner(employeeData));

            PrintAnswer(OneLiner(employeeData));

            PrintAnswer(UseZip(employeeData));

            PrintAnswer(UseExtensions(employeeData));

            while (true) { }
        }

        static void PrintAnswer(Dictionary<string, string> dictionary)
        {
            Console.WriteLine(dictionary.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
        }


        // ZIPワンライナー
        static Dictionary<string, string> ZipOneLiner(List<string> employeeData)
        {
            return employeeData.Zip(employeeData.Skip(1), (key, val) => new { key, val }).Where((x, idx) => idx % 2 == 0).ToDictionary(x => x.key, x => x.val);
        }

        // ワンライナー
        static Dictionary<string, string> OneLiner(List<string> employeeData)
        {
            return employeeData.Select((val, idx) => new { val, idx }).GroupBy(x => (int)(x.idx / 2), x => x.val).ToDictionary(x => x.FirstOrDefault(), x => x.LastOrDefault());
        }

        // 奇数リスト、偶数リストに分けてZIP
        static Dictionary<string, string> UseZip(List<string> employeeData)
        {
            var k = employeeData.Where((val, idx) => (idx % 2 == 0));
            var v = employeeData.Where((val, idx) => (idx % 2 == 1));

            return k.Zip(v, (key, val) => new { key, val }).ToDictionary(x => x.key, x => x.val);
        }

        // 指定個数ずつ取得する拡張メソッド使用
        static Dictionary<string, string> UseExtensions(List<string> employeeData)
        {
            return employeeData.Chunks(2).ToDictionary(x => x.FirstOrDefault(), x => x.LastOrDefault());
        }
    }

    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> list, int size)
        {
            while(list.Any())
            {
                yield return list.Take(size);
                list = list.Skip(size);
            }
        }
    }
}
