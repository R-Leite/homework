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

                // プレイヤークラス
                var players = new List<Player>();

                for (var i = 1; i <= playerNumber; i++)
                {
                    players.Add(new Player(i));
                }

                // ペアクラス
                var pairs = new List<Pair>();

                // 全ペアを作成する
                for(var i = 0; i < playerNumber; i++)
                {
                    for (var j = i + 1; j < playerNumber; j++)
                    {
                        pairs.Add(new Pair(players[i], players[j]));
                    }
                }

                // 全試合組み合わせを作成する
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

                var count = 0;
                while (true)
                {
                    if (count++ > 50) { break; }
                    var priority = int.MaxValue;
                    var index = 0;
                    // 優先順位をつける
                    for (var i = 0; i < matches.Count; i++)
                    {
                        var weight = matches[i].Pair1._player1._participate +
                            matches[i].Pair1._player2._participate +
                            matches[i].Pair2._player1._participate +
                            matches[i].Pair2._player2._participate +
                            matches[i].Pair3._player1._participate +
                            matches[i].Pair3._player2._participate +
                            matches[i].Pair4._player1._participate +
                            matches[i].Pair4._player2._participate +
                            matches[i].Pair1._participate +
                            matches[i].Pair2._participate +
                            matches[i].Pair3._participate +
                            matches[i].Pair4._participate;

                        if (priority > weight)
                        {
                            priority = weight;
                            index = i;
                        }
                     }

                    var x = matches[index];
                    x.Pair1._player1.participateCount();
                    x.Pair1._player2.participateCount();
                    x.Pair2._player1.participateCount();
                    x.Pair2._player2.participateCount();
                    x.Pair3._player1.participateCount();
                    x.Pair3._player2.participateCount();
                    x.Pair4._player1.participateCount();
                    x.Pair4._player2.participateCount();
                    x.Pair1.participateCount();
                    x.Pair2.participateCount();
                    x.Pair3.participateCount();
                    x.Pair4.participateCount();
                    Console.WriteLine(x.Pair1._player1._playerNum.ToString().PadLeft(2,' ') + "," + x.Pair1._player2._playerNum.ToString().PadLeft(2, ' ') + "," + x.Pair2._player1._playerNum.ToString().PadLeft(2, ' ') + "," + x.Pair2._player2._playerNum.ToString().PadLeft(2, ' ') + "," + x.Pair3._player1._playerNum.ToString().PadLeft(2, ' ') + "," + x.Pair3._player2._playerNum.ToString().PadLeft(2, ' ') + "," + x.Pair4._player1._playerNum.ToString().PadLeft(2, ' ') + "," + x.Pair4._player2._playerNum.ToString().PadLeft(2, ' '));

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
//                    Console.WriteLine(mm.Pair1._player1.ToString() + "," + mm.Pair1._player2.ToString() + "," + mm.Pair2._player1.ToString() + "," + mm.Pair2._player2.ToString() + "," + mm.Pair3._player1.ToString() + "," + mm.Pair3._player2.ToString() + "," + mm.Pair4._player1.ToString() + "," + mm.Pair4._player2.ToString());
                    
                    ansMatches.Add(mm);
                    copyMatches.Remove(mm);
                }

                Console.WriteLine();
                ansMatches.ForEach(x => Console.WriteLine(x.Pair1._player1._playerNum+","+x.Pair1._player2._playerNum+","+ x.Pair2._player1._playerNum+ "," + x.Pair2._player2._playerNum + "," + x.Pair3._player1._playerNum+ "," + x.Pair3._player2._playerNum+ "," + x.Pair4._player1._playerNum+ "," + x.Pair4._player2._playerNum));
            }
            finally
            {
                Console.WriteLine("続行するには何かキーを押してください。");
                Console.ReadKey();
            }
        }
    }
}
