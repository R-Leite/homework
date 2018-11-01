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
                Console.Write("人数を入力して下さい。(8以上)：");
                int playerNumber;
                while (true)
                {
                    if(int.TryParse(Console.ReadLine(), out playerNumber))
                    {
                        if (playerNumber >= 8)
                        {
                            break;
                        }
                    }
                    Console.Write("8以上の数字を入力して下さい。：");
                }

                // プレイヤーのリストを作成
                var players = new List<Player>();
                //Enumerable.Range(1, playerNumber).ToList().ForEach(x => players.Add(new Player(x)));
                for (var i = 1; i <= playerNumber; i++)
                {
                    players.Add(new Player(i));
                }

                // 存在しうる全ペアを作成
                var pairs = new List<Pair>();
                //Enumerable.Range(0, playerNumber).ToList().ForEach(x => Enumerable.Range(x + 1, playerNumber - x - 1).ToList().ForEach(y => pairs.Add(new Pair(players[x], players[y]))));
                for (var i = 0; i < playerNumber; i++)
                {
                    for (var j = i + 1; j < playerNumber; j++)
                    {
                        pairs.Add(new Pair(players[i], players[j]));
                    }
                }

                // 存在し得る全試合を作成
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

                // 存在し得る全試合（2面コート）を作成
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

                var count = 0;
                var participateCount = new ParticipateCount(players, pairs, matches);

                while (true)
                {
                    if (count++ > 50) { break; }
                    var minWeight = int.MaxValue;
                    var index = 0;
                    // 優先順位をつける
                    for (var i = 0; i < competitions.Count; i++)
                    {
                        var weight = participateCount.GetWeight(competitions[i]);

                        if (minWeight > weight)
                        {
                            minWeight = weight;
                            index = i;
                        }
                    }
                    var x = competitions[index];
                    if (participateCount.isAllPair(x))
                    {
                        break;
                    }

                    participateCount.Add(x);
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
