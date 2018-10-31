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
            // コンソールサイズ指定
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
                        if (!pairs[i].Contains(pairs[j]))
                        {
                            matches.Add(new Match(pairs[i], pairs[j]));
                        }
                    }
                }

                var competitions = new List<Competition>();
                for (var i = 0; i < matches.Count; i++)
                {
                    for (var j = i + 1; j < matches.Count; j++)
                    {
                        if (!matches[i].Contains(matches[j]))
                        {
                            competitions.Add(new Competition(matches[i], matches[j]));
                        }
                    }
                }

                // ソートしてあげることで同組み合わせかどうか調べる
                var count = 0;
                while (true)
                {
                    if (count++ > 50) { break; }
                    var priority = int.MaxValue;
                    var index = 0;
                    // 優先順位をつける
                    for (var i = 0; i < competitions.Count; i++)
                    {
                        var weight = competitions[i].Match1.Pair1.Player1.Participate +
                            competitions[i].Match1.Pair1.Player2.Participate +
                            competitions[i].Match1.Pair2.Player1.Participate +
                            competitions[i].Match1.Pair2.Player2.Participate +
                            competitions[i].Match2.Pair1.Player1.Participate +
                            competitions[i].Match2.Pair1.Player2.Participate +
                            competitions[i].Match2.Pair2.Player1.Participate +
                            competitions[i].Match2.Pair2.Player2.Participate +
                            competitions[i].Match1.Pair1.Participate +
                            competitions[i].Match1.Pair2.Participate +
                            competitions[i].Match2.Pair1.Participate +
                            competitions[i].Match2.Pair2.Participate;

                        if (priority > weight)
                        {
                            priority = weight;
                            index = i;
                        }
                    }
                    var x = competitions[index];
                    if (x.Match1.Pair1.Participate > 0 & x.Match1.Pair2.Participate > 0 & x.Match2.Pair1.Participate > 0 & x.Match2.Pair2.Participate > 0)
                    {
                        break;
                    }

                    x.ParticipateCount();
                    Console.WriteLine(x.ToString());
                }

            }
            finally
            {
                Console.WriteLine("続行するには何かキーを押してください。");
                Console.ReadKey();
            }
        }
    }
}
