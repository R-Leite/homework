using System;
using System.Linq;

namespace Q10
{
    class Program
    {
        static private char Delimiter = ',';

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("電飾情報を入力して下さい(終了はexit)：");
                var input = Console.ReadLine();

                if (input == "exit") return;

                // 交互列ごとに区切り、列長に変換。（ex.1011011⇒101,101,1⇒3,3,1）操作する連続電球はこの区切り
                var lightLength = input.Aggregate("", (a, b) => (a.LastOrDefault() == b) ? a + Delimiter + b : a + b).Split(Delimiter).Select(x => x.Count());

                // 操作すると前後の操作連続電球を含め交互になるため、操作連続電球と前後の長さを加算し、最大長になるものが答えとなる。
                // 区切り数が3未満ものは全電球が交互列にできるため入力の長さそのまま
                var maxlightLength = lightLength.Select((val, idx) => lightLength.Skip(idx).Take(3).Sum()).Max();

                Console.WriteLine($"交互列最大長：{maxlightLength}");
            }
        }
    }
}
