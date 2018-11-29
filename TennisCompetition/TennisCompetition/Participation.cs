using System;
using System.Collections.Generic;

namespace TennisCompetition
{
    class Participation
    {
        public readonly Dictionary<Player, int> Player;
        public readonly Dictionary<Pair, int> Pair;
        public readonly Dictionary<string, int> courtPair;
        public readonly Dictionary<string, int> Trio;
        public readonly Dictionary<string, int> Match;

        public Participation(List<Player> players, List<Pair> pairs, List<Trio> trios, List<Match> matches)
        {
            this.Player = new Dictionary<Player, int>();
            this.Pair = new Dictionary<Pair, int>();
            this.courtPair = new Dictionary<string, int>();
            this.Trio = new Dictionary<string, int>();
            this.Match = new Dictionary<string, int>();

            players.ForEach(p => this.Player.Add(p, 0));
            pairs.ForEach(p => this.Pair.Add(p, 0));
            pairs.ForEach(p => this.courtPair.Add(p.Label, 0));
            trios.ForEach(t => this.Trio.Add(t.Label, 0));

            foreach(var m in matches)
            {
                if (!this.Match.ContainsKey(m.Label))
                {
                    this.Match.Add(m.Label, 0);
                }
            }
        }

        public int GetWeightFor1(TwoCourts c)
        {
            return this.Match[c.Match1.Label] +
                   this.Match[c.Match2.Label] +
                   this.Pair[c.Match1.Pair1] +
                   this.Pair[c.Match1.Pair2] +
                   this.Pair[c.Match2.Pair1] +
                   this.Pair[c.Match2.Pair2] +
                   this.Player[c.Match1.Pair1.Player1] +
                   this.Player[c.Match1.Pair1.Player2] +
                   this.Player[c.Match1.Pair2.Player1] +
                   this.Player[c.Match1.Pair2.Player2] +
                   this.Player[c.Match2.Pair1.Player1] +
                   this.Player[c.Match2.Pair1.Player2] +
                   this.Player[c.Match2.Pair2.Player1] +
                   this.Player[c.Match2.Pair2.Player2];
        }

        // その組み合わせの出場回数の合計を返す
        public int GetWeight(TwoCourts c)
        {
            return this.Match[c.Match1.Label] +
                   this.Match[c.Match2.Label] +
                   this.Trio[c.Match1.TriosSameCourt[0].Label] +
                   this.Trio[c.Match1.TriosSameCourt[1].Label] +
                   this.Trio[c.Match1.TriosSameCourt[2].Label] +
                   this.Trio[c.Match1.TriosSameCourt[3].Label] +
                   this.Trio[c.Match2.TriosSameCourt[0].Label] +
                   this.Trio[c.Match2.TriosSameCourt[1].Label] +
                   this.Trio[c.Match2.TriosSameCourt[2].Label] +
                   this.Trio[c.Match2.TriosSameCourt[3].Label] +
                   this.courtPair[c.Match1.PairsSameCourt[0].Label] +
                   this.courtPair[c.Match1.PairsSameCourt[1].Label] +
                   this.courtPair[c.Match1.PairsSameCourt[2].Label] +
                   this.courtPair[c.Match1.PairsSameCourt[3].Label] +
                   this.courtPair[c.Match1.PairsSameCourt[4].Label] +
                   this.courtPair[c.Match1.PairsSameCourt[5].Label] +
                   this.courtPair[c.Match2.PairsSameCourt[0].Label] +
                   this.courtPair[c.Match2.PairsSameCourt[1].Label] +
                   this.courtPair[c.Match2.PairsSameCourt[2].Label] +
                   this.courtPair[c.Match2.PairsSameCourt[3].Label] +
                   this.courtPair[c.Match2.PairsSameCourt[4].Label] +
                   this.courtPair[c.Match2.PairsSameCourt[5].Label] +
                   this.Pair[c.Match1.Pair1] +
                   this.Pair[c.Match1.Pair2] +
                   this.Pair[c.Match2.Pair1] +
                   this.Pair[c.Match2.Pair2] +
                   this.Player[c.Match1.Pair1.Player1] +
                   this.Player[c.Match1.Pair1.Player2] +
                   this.Player[c.Match1.Pair2.Player1] +
                   this.Player[c.Match1.Pair2.Player2] +
                   this.Player[c.Match2.Pair1.Player1] +
                   this.Player[c.Match2.Pair1.Player2] +
                   this.Player[c.Match2.Pair2.Player1] +
                   this.Player[c.Match2.Pair2.Player2];
        }

        // 指定組み合わせの出場回数をカウントアップ
        public void CountUp(TwoCourts c)
        {
            this.Match[c.Match1.Label]++;
            this.Match[c.Match2.Label]++;
            this.Trio[c.Match1.TriosSameCourt[0].Label]++;
            this.Trio[c.Match1.TriosSameCourt[1].Label]++;
            this.Trio[c.Match1.TriosSameCourt[2].Label]++;
            this.Trio[c.Match1.TriosSameCourt[3].Label]++;
            this.Trio[c.Match2.TriosSameCourt[0].Label]++;
            this.Trio[c.Match2.TriosSameCourt[1].Label]++;
            this.Trio[c.Match2.TriosSameCourt[2].Label]++;
            this.Trio[c.Match2.TriosSameCourt[3].Label]++;
            this.courtPair[c.Match1.PairsSameCourt[0].Label]++;
            this.courtPair[c.Match1.PairsSameCourt[1].Label]++;
            this.courtPair[c.Match1.PairsSameCourt[2].Label]++;
            this.courtPair[c.Match1.PairsSameCourt[3].Label]++;
            this.courtPair[c.Match1.PairsSameCourt[4].Label]++;
            this.courtPair[c.Match1.PairsSameCourt[5].Label]++;
            this.courtPair[c.Match2.PairsSameCourt[0].Label]++;
            this.courtPair[c.Match2.PairsSameCourt[1].Label]++;
            this.courtPair[c.Match2.PairsSameCourt[2].Label]++;
            this.courtPair[c.Match2.PairsSameCourt[3].Label]++;
            this.courtPair[c.Match2.PairsSameCourt[4].Label]++;
            this.courtPair[c.Match2.PairsSameCourt[5].Label]++;
            this.Pair[c.Match1.Pair1]++;
            this.Pair[c.Match1.Pair2]++;
            this.Pair[c.Match2.Pair1]++;
            this.Pair[c.Match2.Pair2]++;
            this.Player[c.Match1.Pair1.Player1]++;
            this.Player[c.Match1.Pair1.Player2]++;
            this.Player[c.Match1.Pair2.Player1]++;
            this.Player[c.Match1.Pair2.Player2]++;
            this.Player[c.Match2.Pair1.Player1]++;
            this.Player[c.Match2.Pair1.Player2]++;
            this.Player[c.Match2.Pair2.Player1]++;
            this.Player[c.Match2.Pair2.Player2]++;
        }

        public bool isExistTwoMore(TwoCourts c)
        {
            if (this.Trio[c.Match1.TriosSameCourt[0].Label] > 0) { return true; }
            if (this.Trio[c.Match1.TriosSameCourt[1].Label] > 0) { return true; }
            if (this.Trio[c.Match1.TriosSameCourt[2].Label] > 0) { return true; }
            if (this.Trio[c.Match1.TriosSameCourt[3].Label] > 0) { return true; }
            if (this.Trio[c.Match2.TriosSameCourt[0].Label] > 0) { return true; }
            if (this.Trio[c.Match2.TriosSameCourt[1].Label] > 0) { return true; }
            if (this.Trio[c.Match2.TriosSameCourt[2].Label] > 0) { return true; }
            if (this.Trio[c.Match2.TriosSameCourt[3].Label] > 0) { return true; }

            return false;
        }

        // 全ペアが1回以上出場しているか
        public bool isAllPairAtLeastOnce()
        {
            foreach (var key in this.Pair.Keys)
            {
                if (this.Pair[key] <= 0) { return false; }
            }

            return true;
        }

        // 全3人が1回以上で出場しているか
        public bool isAllTrioAtLeastOnce()
        {
            foreach (var key in this.Trio.Keys)
            {
                if (this.Trio[key] <= 0) { return false; }
            }
            return true;
        }

        // 全プレイヤーの出場回数を出力
        public void WriteLinePlayer()
        {
            foreach (var key in this.Player.Keys)
            {
                Console.WriteLine($"{key}={this.Player[key]}");
            }
        }

        // 全ペアの出場回数を出力
        public void WriteLinePair()
        {
            foreach (var key in this.Pair.Keys)
            {
                Console.WriteLine($"{key}={this.Pair[key]}");
            }
        }
    }
}
