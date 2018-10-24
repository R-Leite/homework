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
                var input = Console.ReadLine();
                // プレイヤークラスも必要だとおもう

                // ペアクラス
                var pairs = new List<Pair>();

                // 全ペアを作成する
                for(var i = 1; i <= 8; i++)
                {
                    for (var j = i + 1; j <= 8; j++)
                    {
                        pairs.Add(new Pair(i, j));
                    }
                }

                for (var i = 0; i < pairs.Count; i++)
                {
                    for (var j = i + 1; j < pairs.Count; j++)
                    {
                        if (!pairs[i].contains(pairs[j]))
                        {
                            Console.WriteLine(pairs[i]._player1.ToString() + ", " + pairs[i]._player2.ToString());
                            Console.WriteLine(pairs[j]._player1.ToString() + ", " + pairs[j]._player2.ToString());
                            Console.WriteLine();
                        }
                    }
                }
                //pairs.ForEach(x => Console.WriteLine(x._player1.ToString() +","+x._player2.ToString()));

                // ペアを4つ組み合わせて、全ペアがなくなるまで繰り返す
                var cpairs = pairs.ToList();
                var answers = new List<Pair>();
                while (cpairs.Count() > 0)
                {
                    // 重複チェックListが4の倍数（0含む）ならそのまま追加
                    if (answers.Count % 4 == 0)
                    {
                        var fuga = cpairs.First();
                        answers.Add(fuga);
                        cpairs.Remove(fuga);
                        continue;
                    }

//                    Console.WriteLine(answers.Count);
                    var skip = answers.Count / 4 * 4;
//                    Console.WriteLine(skip);
                    var hoge = cpairs.Where(x => !answers.Skip(answers.Count / 4 * 4).Any(y => y.contains(x))).First();
                    Console.WriteLine(hoge._player1.ToString() + "," + hoge._player2.ToString());
                    //                    answers.Add(cpairs.Where(x => answers.Any(y => !y.contains(x))).First());
                    answers.Add(hoge);
                    cpairs.Remove(hoge);

                    // 重複チェック用Listのlengthが4ならclear
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
