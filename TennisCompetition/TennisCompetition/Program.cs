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

                var trios = new List<Trio>();
                for (var i = 0; i < playerNumber; i++)
                {
                    for (var j = i + 1; j < playerNumber; j++)
                    {
                        for (var k = j + 1; k < playerNumber; k++)
                        {
                            trios.Add(new Trio(players[i], players[j], players[k]));
                        }
                    }
                }


                // 存在し得る全試合を作成
                var pairCount = pairs.Count;
                var matches = new List<Match>();
                for (var i = 0; i < pairCount; i++)
                {
                    for (var j = i + 1; j < pairCount; j++)
                    {
                        // プレイヤーの重複を除く
                        if (!pairs[i].Contains(pairs[j]))
                        {
                            matches.Add(new Match(pairs[i], pairs[j]));
                        }
                    }
                }

                // 存在し得る全試合（2面コート）を作成
                var matchCount = matches.Count;
                var competitions = new List<Competition>();
                for (var i = 0; i < matchCount; i++)
                {
                    for (var j = i + 1; j < matchCount; j++)
                    {
                        // プレイヤーの重複を除く
                        if (!matches[i].Contains(matches[j]))
                        {
                            competitions.Add(new Competition(matches[i], matches[j]));
                        }
                    }
                }

                // 出場回数の管理
                var participation = new Participation(players, pairs, trios, matches);
                while (true)
                {
                    var minWeight = int.MaxValue;
                    var index = 0;
                    // 出場回数から優先順位をつける
                    for (var i = 0; i < competitions.Count; i++)
                    {
                        var weight = participation.GetWeight(competitions[i]);

                        if (minWeight > weight)
                        {
                            minWeight = weight;
                            index = i;
                        }
                    }

                    var x = competitions[index];

                    // 全ペアが出場したら終了
                    if (participation.isAllPair(x))
                    {
                        break;
                    }

                    participation.Add(x);
                    Console.WriteLine(x.ToString());
                    Console.WriteLine(
                        x.Match1.Pair1.Player1.Label.ToString() + "," +
                        x.Match1.Pair1.Player2.Label.ToString() + "," +
                        x.Match1.Pair2.Player1.Label.ToString() + "," +
                        x.Match1.Pair2.Player2.Label.ToString() + "," +
                        x.Match2.Pair1.Player1.Label.ToString() + "," +
                        x.Match2.Pair1.Player2.Label.ToString() + "," +
                        x.Match2.Pair2.Player1.Label.ToString() + "," +
                        x.Match2.Pair2.Player2.Label.ToString() + ":" +
                        //
                        participation.Player[x.Match1.Pair1.Player1]+ "," +
                        participation.Player[x.Match1.Pair1.Player2] + "," +
                        participation.Player[x.Match1.Pair2.Player1] + "," +
                        participation.Player[x.Match1.Pair2.Player2] + "," +
                        participation.Player[x.Match2.Pair1.Player1] + "," +
                        participation.Player[x.Match2.Pair1.Player2] + "," +
                        participation.Player[x.Match2.Pair2.Player1] + "," +
                        participation.Player[x.Match2.Pair2.Player2] 
                    );
                }
                foreach(var key in participation.Player.Keys)
                {
                    Console.WriteLine($"{key}={participation.Player[key]}");
                }
                foreach (var key in participation.Pair.Keys)
                {
                    Console.WriteLine($"{key}={participation.Pair[key]}");
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
