using System;
using System.Collections.Generic;
using System.Linq;

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
                // 人数入力
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
                // ex:[1,2,3,...,8]
                var players = Enumerable.Range(1, playerNumber).Select(x => new Player(x)).ToList();

                // 存在しうる全ペアを作成
                // ex:[(1,2),(1,3),...,(1,8),(2,3),(2,4),...,(7,8)]
                var pairs = players.SelectMany((x, idx) =>
                players.Skip(idx + 1).Select(y => new Pair(x, y))).ToList();

                // 同コート上のプレイヤー確認用
                // ex:[(1,2,3),(1,2,4),...,(1,7,8),(2,3,4),(2,3,5),...,(6,7,8)]
                var trios = players.SelectMany((x, idx) =>
                players.Skip(idx + 1).SelectMany((y, idy) =>
                players.Skip(idx + idy + 2).Select(z => new Trio(x, y, z)))).ToList();

                // 存在し得る全試合を作成
                // ex:[(1,2,3,4),(1,2,3,5),...,(2,3,4,5),(2,3,4,6)},...,{(5,6),(7,8)}]
                var matches = pairs.SelectMany((x, idx) => pairs.Skip(idx + 1).Where(y=>!x.Contains(y)).Select(y => new Match(x, y))).ToList();

                // 存在し得る全試合（2面コート）を作成
                // ex:[(1,2,3,4,5,6,7,8),(1,2,3,4,5,6,8),...,(1,8,2,7,3,6,4,5)]
                var twoCourtMatches = matches.SelectMany((x, idx) => matches.Skip(idx + 1).Where(y => !x.Contains(y)).Select(y => new TwoCourts(x, y))).ToList();

                // 出場回数の管理
                var participation1 = new Participation(players, pairs, trios, matches);
                
                // 1の回答出力
                //var ans1 = new Answer1(twoCourtsList, participation1);
                //ans1.Output();

                Console.WriteLine("\n2番の回答へ（何かキーを押してください。)");
                Console.ReadKey();
                Console.WriteLine();

                // 出場回数の管理
                var participation = new Participation(players, pairs, trios, matches);

                // 2以降の回答
                var ans = new Answer(twoCourtMatches, participation);
                ans.Output();
            }
            finally
            {
                Console.WriteLine("続行するには何かキーを押してください。");
                Console.ReadKey();
            }
        }
    }
}
