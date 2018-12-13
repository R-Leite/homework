using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisCompetition
{
    // 出場回数を管理するクラス
    class Participation
    {
        public readonly Dictionary<int, int> Player;
        public readonly Dictionary<string, int> Pair;
        public readonly Dictionary<string, int> courtPair;
        public readonly Dictionary<string, int> Trio;
        public readonly Dictionary<string, int> Match;

        public Participation(IEnumerable<Player> players, IEnumerable<Pair> pairs, IEnumerable<Trio> trios, IEnumerable<Match> matches)
        {
            // 出場回数を0に初期化
            this.Player = players.ToDictionary(x => x.Label, x => 0);
            this.Pair = pairs.ToDictionary(x => x.Label, x => 0);
            this.courtPair = pairs.ToDictionary(x =>x.Label, x => 0);
            this.Trio = trios.ToDictionary(x => x.Label, x => 0);
            this.Match = matches.Select(x => x.Label).Distinct().ToDictionary(x => x, x => 0);
        }

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
                this.GetWeightSameCourtThree(m) +
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


        // 同じ3人がコート上に存在しているか
        public bool isExistTwoMore(MultiMatch m)
        {
            if (m.Match1.TriosSameCourt.Any(x => this.Trio[x.Label] > 0)) { return true; }
            if (m.Match2.TriosSameCourt.Any(x => this.Trio[x.Label] > 0)) { return true; }
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
            return (this.Trio.Values.Any(v => v <= 0)) ? false : true;
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
                c.Match1.TriosSameCourt.Sum(x => this.Trio[x.Label]) +
                c.Match2.TriosSameCourt.Sum(x => this.Trio[x.Label]);
        }

        // 同コート内での2人組み合わせの出場回数合計
        private int GetWeightSameCourtTwo(MultiMatch c)
        {
            return
                c.Match1.PairsSameCourt.Sum(x => this.courtPair[x.Label]) +
                c.Match2.PairsSameCourt.Sum(x => this.courtPair[x.Label]);
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
            m.Match1.TriosSameCourt.ToList().ForEach(x => this.Trio[x.Label]++);
            m.Match2.TriosSameCourt.ToList().ForEach(x => this.Trio[x.Label]++);
        }

        // 同コートの2人出場回数カウントアップ
        private void CountUpSameCourtTwo(MultiMatch m)
        {
            m.Match1.PairsSameCourt.ToList().ForEach(x => this.courtPair[x.Label]++);
            m.Match2.PairsSameCourt.ToList().ForEach(x => this.courtPair[x.Label]++);
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
