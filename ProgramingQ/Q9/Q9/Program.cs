using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Q9
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("S,P,Eの3種類からなる文字列を入力してください(終了はexit)");
                var input = Console.ReadLine();

                if (input == "exit") return;

#if false
                //var maxLevel = Enumerable.Range(1, 1000000).OrderByDescending(x => x).Where(x => new Regex("S{x}P{x}E{x}").IsMatch(input)).FirstOrDefault();
#else
                var matchCollection = new Regex("S+P+E+").Matches(input).AsEnumerable().Select(x => x.Value)
                    .Where(x => x.Count(c => c == 'P') <= x.Count(c => c == 'S'))
                    .Where(x => x.Count(c => c == 'P') <= x.Count(c => c == 'E'));
                var maxLevel = (matchCollection.Count() <= 0) ? 0 : matchCollection.Select(x => x.Count(c => c == 'P')).Max();
#endif
                Console.WriteLine($"最大Level：{maxLevel}");
                Console.WriteLine();
            }
        }
    }

    public static class RegexExtension
    {
        public static IEnumerable<Match> AsEnumerable(this MatchCollection mc)
        {
            foreach(Match m in mc)
            {
                yield return m;
            }
        }
    }
}
