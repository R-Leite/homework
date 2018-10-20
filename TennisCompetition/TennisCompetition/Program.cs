using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;

            try
            {
                Console.WriteLine("テニスの対戦組み合わせ表を出力します");
                var pairs = new List<Pair>();

                // 全ペアを作成する
                for(var i = 1; i <= 8; i++)
                {
                    for (var j = i + 1; j <= 8; j++)
                    {
                        pairs.Add(new Pair(i, j));
                    }
                }

                //pairs.ForEach(x => Console.WriteLine(x._player1.ToString() +","+x._player2.ToString()));

                // ペアを4つ組み合わせて、全ペアがなくなるまで繰り返す
                var cpairs = pairs.ToList();
                var answers = new List<Pair>();
                answers.Add(new Pair(1, 2));
                while (cpairs.Count() > 0)
                {
                    var hoge = cpairs.Where(x => !answers.Any(y => y.contains(x))).First();
                    Console.WriteLine(hoge._player1.ToString() + "," + hoge._player2.ToString());
                    //                    answers.Add(cpairs.Where(x => answers.Any(y => !y.contains(x))).First());
                    answers.Add(hoge);
                    cpairs.Remove(hoge);
                }
                Console.WriteLine();
                answers.ForEach(x => Console.WriteLine(x._player1.ToString() + "," + x._player2.ToString()));

            }
            finally
            {
                Console.WriteLine("続行するには何かキーを押してください。");
                Console.ReadKey();
            }
        }
    }
}
