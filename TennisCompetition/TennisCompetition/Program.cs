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
                Console.WriteLine("テニスの対戦組み合わせ表を出力します。");
                Console.Write("人数を入力して下さい。：");
                var playerNumber = int.Parse(Console.ReadLine());

                // プレイヤークラスも必要だとおもう

                // ペアクラス
                var pairs = new List<Pair>();

                // 全ペアを作成する
                for(var i = 1; i <= playerNumber; i++)
                {
                    for (var j = i + 1; j <= playerNumber; j++)
                    {
                        pairs.Add(new Pair(i, j));
                    }
                }

                var matches = new List<Match>();
                for (var i = 0; i < pairs.Count; i++)
                {
                    for (var j = i + 1; j < pairs.Count; j++)
                    {
                        for (var k = j + 1; k < pairs.Count; k++)
                        {
                            for (var l = k + 1; l < pairs.Count; l++)
                            {
                                if (!pairs[i].contains(pairs[j]) && !pairs[i].contains(pairs[k]) && !pairs[i].contains(pairs[l]))
                                {
                                    if (!pairs[j].contains(pairs[k]) && !pairs[j].contains(pairs[l]))
                                    {
                                        if (!pairs[k].contains(pairs[l]))
                                        {
                                            matches.Add(new Match(pairs[i], pairs[j], pairs[k], pairs[l]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var copyMatches = matches.ToList();
                var ansMatches = new List<Match>();

                while (true)
                {
                    if (ansMatches.Count == 0)
                    {
                        var m = copyMatches.First();
                        ansMatches.Add(m);
                        copyMatches.Remove(m);
                        continue;
                    }

                    var mm = copyMatches.Where(x => !ansMatches.Any(y=>y.contains(x))).FirstOrDefault();
                    if (mm == null)
                    {
                        break;
                    }
                    Console.WriteLine(mm.Pair1._player1.ToString() + "," + mm.Pair1._player2.ToString() + "," + mm.Pair2._player1.ToString() + "," + mm.Pair2._player2.ToString() + "," + mm.Pair3._player1.ToString() + "," + mm.Pair3._player2.ToString() + "," + mm.Pair4._player1.ToString() + "," + mm.Pair4._player2.ToString());
                    
                    ansMatches.Add(mm);
                    copyMatches.Remove(mm);
                }

                Console.WriteLine();
                ansMatches.ForEach(x => Console.WriteLine(x.Pair1._player1.ToString()+","+x.Pair1._player2.ToString()+","+ x.Pair2._player1.ToString() + "," + x.Pair2._player2.ToString() + "," + x.Pair3._player1.ToString() + "," + x.Pair3._player2.ToString() + "," + x.Pair4._player1.ToString() + "," + x.Pair4._player2.ToString()));

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
                    //Console.WriteLine(hoge._player1.ToString() + "," + hoge._player2.ToString());
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
