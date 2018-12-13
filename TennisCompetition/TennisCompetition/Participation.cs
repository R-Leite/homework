using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisCompetition
{
    // 出場回数を管理するクラス
    class Participation
    {
        public readonly Dictionary<int, int> Player;
        private readonly Dictionary<string, int> Pair;
        private readonly Dictionary<string, int> SameCourtTwo;
        private readonly Dictionary<string, int> SameCourtThree;
        private readonly Dictionary<string, int> Match;

        public Participation(IEnumerable<Player> players, IEnumerable<Pair> pairs, IEnumerable<Trio> trios, IEnumerable<Match> matches)
        {
            // 出場回数を0に初期化
            this.Player = players.ToDictionary(x => x.Label, x => 0);
            this.Pair = pairs.ToDictionary(x => x.Label, x => 0);
            this.SameCourtTwo = pairs.ToDictionary(x =>x.Label, x => 0);
            this.SameCourtThree = trios.ToDictionary(x => x.Label, x => 0);
            this.Match = matches.Select(x => x.Label).Distinct().ToDictionary(x => x, x => 0);
        }

        // その組み合わせの出場回数の合計を返す(設問1用)
        public int GetWeightForAnswer1(MultiMatch m)
        {
            return
                this.GetWeightMatch(m) +
                this.GetWeightPair(m) +
                this.GetWeightPlayer(m);
        }

        // その組み合わせの出場回数の合計を返す
        public int GetWeight(MultiMatch m)
        {
            return
                this.GetWeightMatch(m) +
                //this.GetWeightSameCourtThree(m) +
                this.GetWeightSameCourtTwo(m) +
                this.GetWeightPair(m) +
                this.GetWeightPlayer(m);
        }

        // 指定組み合わせの出場回数をカウントアップ
        public void CountUp(MultiMatch m)
        {
            this.CountUpMatch(m);
            this.CountUpSameCourtThree(m);
            this.CountUpSameCourtTwo(m);
            this.CountUpPair(m);
            this.CountUpPlayer(m);
        }

        public IEnumerable<int> GeteightPlayer()
        {
            var hoge = this.Player.OrderBy(x => x.Value).Take(8).Select(x=>x.Key);
            Console.Write(hoge.Select(x => x.ToString()).Aggregate((a, b) => a + "-" + b));
            Console.WriteLine();
            return hoge;
        }

        //public IEnumerable<Pair> GetPair()
        //{
        //    var hoge = this.Pair.OrderBy(x => x.Value).Select(x => x.Key);
        //    foreach(var h in hoge)
        //    {
                
        //    }
        //}

        // 過去に顔を合わせた3人がコート上に存在しているか
        public bool isExistTwoMore(MultiMatch m)
        {
            if (m.Match1.TriosSameCourt.Any(x => this.SameCourtThree[x.Label] > 0)) { return true; }
            if (m.Match2.TriosSameCourt.Any(x => this.SameCourtThree[x.Label] > 0)) { return true; }
            return false;
        }

        // 全ペアが1回以上出場しているか
        public bool isAllPairAtLeastOnce()
        {
            return (this.Pair.Values.Any(v => v <= 0)) ? false : true;
        }

        // 全3人が1回以上で出場しているか
        public bool isAllTrioAtLeastOnce()
        {
            return (this.SameCourtThree.Values.Any(v => v <= 0)) ? false : true;
        }

        // 全プレイヤーの出場回数を出力
        public void WriteLinePlayer()
        {
            foreach (var key in this.Player.Keys)
            {
                var player = new Player(key);
                Console.WriteLine($"{player.ToString()}={this.Player[key]}");
            }
        }

        // 全ペアの出場回数を出力
        public void WriteLinePair()
        {
            foreach (var key in this.Pair.Keys)
            {
                var splitKey = key.Split('-');
                var pair = new Pair(new Player(int.Parse(splitKey[0])), new Player(int.Parse(splitKey[1])));
                Console.WriteLine($"{pair.ToString()}={this.Pair[key]}");
            }
        }

        public void WriteLineTrio()
        {
            foreach (var key in this.SameCourtThree.Keys)
            {
                var sk = key.Split('-');
                var trio = new Trio(new Player(int.Parse(sk[0])), new Player(int.Parse(sk[1])), new Player(int.Parse(sk[2])));
                Console.WriteLine($"{trio.ToString()}={this.SameCourtThree[key]}");
            }
        }

        #region private methods
        // 指定試合の出場回数合計
        private int GetWeightMatch(MultiMatch m)
        {
            return
                this.Match[m.Match1.Label] +
                this.Match[m.Match2.Label];
        }

        // 同コート内での3人組み合わせの出場回数合計
        private int GetWeightSameCourtThree(MultiMatch c)
        {
            return
                c.Match1.TriosSameCourt.Sum(x => this.SameCourtThree[x.Label]) +
                c.Match2.TriosSameCourt.Sum(x => this.SameCourtThree[x.Label]);
        }

        // 同コート内での2人組み合わせの出場回数合計
        private int GetWeightSameCourtTwo(MultiMatch c)
        {
            return
                c.Match1.PairsSameCourt.Sum(x => this.SameCourtTwo[x.Label]) +
                c.Match2.PairsSameCourt.Sum(x => this.SameCourtTwo[x.Label]);
        }

        // ペアの出場回数合計
        private int GetWeightPair(MultiMatch c)
        {
            return
                this.Pair[c.Match1.Pair1.Label] +
                this.Pair[c.Match1.Pair2.Label] +
                this.Pair[c.Match2.Pair1.Label] +
                this.Pair[c.Match2.Pair2.Label];
        }

        // プレイヤーの出場回数合計
        private int GetWeightPlayer(MultiMatch m)
        {
            return
                this.Player[m.Match1.Pair1.Player1.Label] +
                this.Player[m.Match1.Pair1.Player2.Label] +
                this.Player[m.Match1.Pair2.Player1.Label] +
                this.Player[m.Match1.Pair2.Player2.Label] +
                this.Player[m.Match2.Pair1.Player1.Label] +
                this.Player[m.Match2.Pair1.Player2.Label] +
                this.Player[m.Match2.Pair2.Player1.Label] +
                this.Player[m.Match2.Pair2.Player2.Label];
        }

        // 試合の出場回数カウントアップ
        private void CountUpMatch(MultiMatch m)
        {
            this.Match[m.Match1.Label]++;
            this.Match[m.Match2.Label]++;
        }

        // 同コードの3人出場回数カウントアップ
        private void CountUpSameCourtThree(MultiMatch m)
        {
            m.Match1.TriosSameCourt.ToList().ForEach(x => this.SameCourtThree[x.Label]++);
            m.Match2.TriosSameCourt.ToList().ForEach(x => this.SameCourtThree[x.Label]++);
        }

        // 同コートの2人出場回数カウントアップ
        private void CountUpSameCourtTwo(MultiMatch m)
        {
            m.Match1.PairsSameCourt.ToList().ForEach(x => this.SameCourtTwo[x.Label]++);
            m.Match2.PairsSameCourt.ToList().ForEach(x => this.SameCourtTwo[x.Label]++);
        }

        // ペアの出場回数カウントアップ
        private void CountUpPair(MultiMatch m)
        {
            this.Pair[m.Match1.Pair1.Label]++;
            this.Pair[m.Match1.Pair2.Label]++;
            this.Pair[m.Match2.Pair1.Label]++;
            this.Pair[m.Match2.Pair2.Label]++;
        }

        // プレイヤーの出場回数カウントアップ
        private void CountUpPlayer(MultiMatch m)
        {
            this.Player[m.Match1.Pair1.Player1.Label]++;
            this.Player[m.Match1.Pair1.Player2.Label]++;
            this.Player[m.Match1.Pair2.Player1.Label]++;
            this.Player[m.Match1.Pair2.Player2.Label]++;
            this.Player[m.Match2.Pair1.Player1.Label]++;
            this.Player[m.Match2.Pair1.Player2.Label]++;
            this.Player[m.Match2.Pair2.Player1.Label]++;
            this.Player[m.Match2.Pair2.Player2.Label]++;
        }
        #endregion
    }
}
