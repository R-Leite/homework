using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "CODE O!CDE? A?JM!.";

            var testCase = input.Split(' ');

            // 記数法のリスト作成
            var list = testCase.Skip(1).First();
            var aflist = testCase.Skip(2).First();

            // 入力文字を文字から数字に変換（Foo ⇒ 100）
            var hoge = testCase.First().Select(x => list.IndexOf(x));

            // 入力文字を10進数に変換
            var fuga = hoge.Aggregate((a, b) => a * list.Count() + b);
            Console.WriteLine(fuga);

            var foo = ConvertTo(fuga, aflist.Count(), new List<int>());

            // 10⇒与えられた記数法
            var bar = foo.Select(x => aflist[x]);

            Console.WriteLine(bar.Aggregate("",(a, b) => a + "," + b));

            Console.ReadKey();
        }

        static IEnumerable<int> ConvertTo(int number, int dec, IEnumerable<int> convertList)
        {
            if (number < dec)
            {
                return convertList.Concat(Enumerable.Repeat(number, 1)).Reverse();
            }

            return ConvertTo(number / dec, dec, convertList.Concat(Enumerable.Repeat((number % dec), 1)));
        }
    }
}
