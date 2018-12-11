using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisCompetition
{
    // 出場回数を管理するクラス
    class Participation
    {
        public readonly Dictionary<string, int> Player;
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
            this.Match = matches.Select(m => m.Label).Distinct().ToDictionary(x => x, x => 0);
        }

        public int GetWeightFor1(TwoMatch c)
        {
            return this.Match[c.Match1.Label] +
                   this.Match[c.Match2.Label] +
                   this.Pair[c.Match1.Pair1.Label] +
                   this.Pair[c.Match1.Pair2.Label] +
                   this.Pair[c.Match2.Pair1.Label] +
                   this.Pair[c.Match2.Pair2.Label] +
                   this.Player[c.Match1.Pair1.Player1.Label] +
                   this.Player[c.Match1.Pair1.Player2.Label] +
                   this.Player[c.Match1.Pair2.Player1.Label] +
                   this.Player[c.Match1.Pair2.Player2.Label] +
                   this.Player[c.Match2.Pair1.Player1.Label] +
                   this.Player[c.Match2.Pair1.Player2.Label] +
                   this.Player[c.Match2.Pair2.Player1.Label] +
                   this.Player[c.Match2.Pair2.Player2.Label];
        }

        // その組み合わせの出場回数の合計を返す
        public int GetWeight(TwoMatch c)
        {
            return this.Match[c.Match1.Label] +
                   this.Match[c.Match2.Label] +
                   this.trioweight(c) +
                   this.pairweight(c) +
                   this.pweight(c) +
                   this.Player[c.Match1.Pair1.Player1.Label] +
                   this.Player[c.Match1.Pair1.Player2.Label] +
                   this.Player[c.Match1.Pair2.Player1.Label] +
                   this.Player[c.Match1.Pair2.Player2.Label] +
                   this.Player[c.Match2.Pair1.Player1.Label] +
                   this.Player[c.Match2.Pair1.Player2.Label] +
                   this.Player[c.Match2.Pair2.Player1.Label] +
                   this.Player[c.Match2.Pair2.Player2.Label];
        }

        public int trioweight(TwoMatch c)
        {
            return c.Match1.TriosSameCourt.Select(x => this.Trio[x.Label]).Sum() +
                   c.Match2.TriosSameCourt.Select(x => this.Trio[x.Label]).Sum();
            //this.Trio[c.Match1.TriosSameCourt[0].Label] +
            //this.Trio[c.Match1.TriosSameCourt[1].Label] +
            //this.Trio[c.Match1.TriosSameCourt[2].Label] +
            //this.Trio[c.Match1.TriosSameCourt[3].Label] +
            //this.Trio[c.Match2.TriosSameCourt[0].Label] +
            //this.Trio[c.Match2.TriosSameCourt[1].Label] +
            //this.Trio[c.Match2.TriosSameCourt[2].Label] +
            //this.Trio[c.Match2.TriosSameCourt[3].Label] +
        }

        public int pairweight(TwoMatch c)
        {
            return c.Match1.PairsSameCourt.Sum(x => this.courtPair[x.Label]) +
                   c.Match2.PairsSameCourt.Sum(x => this.courtPair[x.Label]);
            //this.courtPair[c.Match1.PairsSameCourt[0].Label] +
            //this.courtPair[c.Match1.PairsSameCourt[1].Label] +
            //this.courtPair[c.Match1.PairsSameCourt[2].Label] +
            //this.courtPair[c.Match1.PairsSameCourt[3].Label] +
            //this.courtPair[c.Match1.PairsSameCourt[4].Label] +
            //this.courtPair[c.Match1.PairsSameCourt[5].Label] +
            //this.courtPair[c.Match2.PairsSameCourt[0].Label] +
            //this.courtPair[c.Match2.PairsSameCourt[1].Label] +
            //this.courtPair[c.Match2.PairsSameCourt[2].Label] +
            //this.courtPair[c.Match2.PairsSameCourt[3].Label] +
            //this.courtPair[c.Match2.PairsSameCourt[4].Label] +
            //this.courtPair[c.Match2.PairsSameCourt[5].Label] +
        }

        public int pweight(TwoMatch c)
        {
            return this.Pair[c.Match1.Pair1.Label] +
                   this.Pair[c.Match1.Pair2.Label] +
                   this.Pair[c.Match2.Pair1.Label] +
                   this.Pair[c.Match2.Pair2.Label];
        }

        // 指定組み合わせの出場回数をカウントアップ
        public void CountUp(TwoMatch c)
        {
            this.Match[c.Match1.Label]++;
            this.Match[c.Match2.Label]++;
            c.Match1.TriosSameCourt.Select(x => this.Trio[x.Label]++);
            c.Match2.TriosSameCourt.Select(x => this.Trio[x.Label]++);
            //this.Trio[c.Match1.TriosSameCourt[0].Label]++;
            //this.Trio[c.Match1.TriosSameCourt[1].Label]++;
            //this.Trio[c.Match1.TriosSameCourt[2].Label]++;
            //this.Trio[c.Match1.TriosSameCourt[3].Label]++;
            //this.Trio[c.Match2.TriosSameCourt[0].Label]++;
            //this.Trio[c.Match2.TriosSameCourt[1].Label]++;
            //this.Trio[c.Match2.TriosSameCourt[2].Label]++;
            //this.Trio[c.Match2.TriosSameCourt[3].Label]++;
            c.Match1.PairsSameCourt.Select(x => this.courtPair[x.Label]++);
            c.Match2.PairsSameCourt.Select(x => this.courtPair[x.Label]++);
            //this.courtPair[c.Match1.PairsSameCourt[0].Label]++;
            //this.courtPair[c.Match1.PairsSameCourt[1].Label]++;
            //this.courtPair[c.Match1.PairsSameCourt[2].Label]++;
            //this.courtPair[c.Match1.PairsSameCourt[3].Label]++;
            //this.courtPair[c.Match1.PairsSameCourt[4].Label]++;
            //this.courtPair[c.Match1.PairsSameCourt[5].Label]++;
            //this.courtPair[c.Match2.PairsSameCourt[0].Label]++;
            //this.courtPair[c.Match2.PairsSameCourt[1].Label]++;
            //this.courtPair[c.Match2.PairsSameCourt[2].Label]++;
            //this.courtPair[c.Match2.PairsSameCourt[3].Label]++;
            //this.courtPair[c.Match2.PairsSameCourt[4].Label]++;
            //this.courtPair[c.Match2.PairsSameCourt[5].Label]++;
            this.Pair[c.Match1.Pair1.Label]++;
            this.Pair[c.Match1.Pair2.Label]++;
            this.Pair[c.Match2.Pair1.Label]++;
            this.Pair[c.Match2.Pair2.Label]++;
            this.Player[c.Match1.Pair1.Player1.Label]++;
            this.Player[c.Match1.Pair1.Player2.Label]++;
            this.Player[c.Match1.Pair2.Player1.Label]++;
            this.Player[c.Match1.Pair2.Player2.Label]++;
            this.Player[c.Match2.Pair1.Player1.Label]++;
            this.Player[c.Match2.Pair1.Player2.Label]++;
            this.Player[c.Match2.Pair2.Player1.Label]++;
            this.Player[c.Match2.Pair2.Player2.Label]++;
        }

        public bool isExistTwoMore(TwoMatch c)
        {
            if (c.Match1.TriosSameCourt.Any(x => this.Trio[x.Label] > 0)) { return true; }
            if (c.Match2.TriosSameCourt.Any(x => this.Trio[x.Label] > 0)) { return true; }
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
                Console.WriteLine($"{key.ToString()}={this.Player[key]}");
            }
        }

        // 全ペアの出場回数を出力
        public void WriteLinePair()
        {
            foreach (var key in this.Pair.Keys)
            {
                Console.WriteLine($"{key.ToString()}={this.Pair[key]}");
            }
        }
    }
}
