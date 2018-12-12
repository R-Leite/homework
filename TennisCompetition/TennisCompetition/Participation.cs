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
            this.Player = players.ToDictionary(x => x.ToString(), x => 0);
            this.Pair = pairs.ToDictionary(x => x.ToString(), x => 0);
            this.courtPair = pairs.ToDictionary(x =>x.ToString(), x => 0);
            this.Trio = trios.ToDictionary(x => x.ToString(), x => 0);
            this.Match = matches.Select(x => x.ToString()).Distinct().ToDictionary(x => x, x => 0);
        }

        public int GetWeightFor1(MultiMatch c)
        {
            return this.Match[c.Match1.ToString()] +
                   this.Match[c.Match2.ToString()] +
                   this.Pair[c.Match1.Pair1.ToString()] +
                   this.Pair[c.Match1.Pair2.ToString()] +
                   this.Pair[c.Match2.Pair1.ToString()] +
                   this.Pair[c.Match2.Pair2.ToString()] +
                   this.Player[c.Match1.Pair1.Player1.ToString()] +
                   this.Player[c.Match1.Pair1.Player2.ToString()] +
                   this.Player[c.Match1.Pair2.Player1.ToString()] +
                   this.Player[c.Match1.Pair2.Player2.ToString()] +
                   this.Player[c.Match2.Pair1.Player1.ToString()] +
                   this.Player[c.Match2.Pair1.Player2.ToString()] +
                   this.Player[c.Match2.Pair2.Player1.ToString()] +
                   this.Player[c.Match2.Pair2.Player2.ToString()];
        }

        // その組み合わせの出場回数の合計を返す
        public int GetWeight(MultiMatch c)
        {
            return this.Match[c.Match1.ToString()] +
                   this.Match[c.Match2.ToString()] +
                   this.trioweight(c) +
                   this.pairweight(c) +
                   this.pweight(c) +
                   this.Player[c.Match1.Pair1.Player1.ToString()] +
                   this.Player[c.Match1.Pair1.Player2.ToString()] +
                   this.Player[c.Match1.Pair2.Player1.ToString()] +
                   this.Player[c.Match1.Pair2.Player2.ToString()] +
                   this.Player[c.Match2.Pair1.Player1.ToString()] +
                   this.Player[c.Match2.Pair1.Player2.ToString()] +
                   this.Player[c.Match2.Pair2.Player1.ToString()] +
                   this.Player[c.Match2.Pair2.Player2.ToString()];
        }

        public int trioweight(MultiMatch c)
        {
#if false
            return c.Match1.TriosSameCourt.Select(x => this.Trio[x.Label]).Sum() +
                   c.Match2.TriosSameCourt.Select(x => this.Trio[x.Label]).Sum();
#else
            return
                this.Trio[c.Match1.TriosSameCourt.First().ToString()] +
                this.Trio[c.Match1.TriosSameCourt.Skip(1).First().ToString()] +
                this.Trio[c.Match1.TriosSameCourt.Skip(2).First().ToString()] +
                this.Trio[c.Match1.TriosSameCourt.Skip(3).First().ToString()] +
                this.Trio[c.Match2.TriosSameCourt.First().ToString()] +
                this.Trio[c.Match2.TriosSameCourt.Skip(1).First().ToString()] +
                this.Trio[c.Match2.TriosSameCourt.Skip(2).First().ToString()] +
                this.Trio[c.Match2.TriosSameCourt.Skip(3).First().ToString()];
#endif
        }

        public int pairweight(MultiMatch c)
        {
#if false
            return c.Match1.PairsSameCourt.Sum(x => this.courtPair[x.Label]) +
                   c.Match2.PairsSameCourt.Sum(x => this.courtPair[x.Label]);
#else
            return
                this.courtPair[c.Match1.PairsSameCourt.Skip(0).First().ToString()] +
                this.courtPair[c.Match1.PairsSameCourt.Skip(1).First().ToString()] +
                this.courtPair[c.Match1.PairsSameCourt.Skip(2).First().ToString()] +
                this.courtPair[c.Match1.PairsSameCourt.Skip(3).First().ToString()] +
                this.courtPair[c.Match1.PairsSameCourt.Skip(4).First().ToString()] +
                this.courtPair[c.Match1.PairsSameCourt.Skip(5).First().ToString()] +
                this.courtPair[c.Match2.PairsSameCourt.Skip(0).First().ToString()] +
                this.courtPair[c.Match2.PairsSameCourt.Skip(1).First().ToString()] +
                this.courtPair[c.Match2.PairsSameCourt.Skip(2).First().ToString()] +
                this.courtPair[c.Match2.PairsSameCourt.Skip(3).First().ToString()] +
                this.courtPair[c.Match2.PairsSameCourt.Skip(4).First().ToString()] +
                this.courtPair[c.Match2.PairsSameCourt.Skip(5).First().ToString()];
#endif
        }

        public int pweight(MultiMatch c)
        {
            return this.Pair[c.Match1.Pair1.Label] +
                   this.Pair[c.Match1.Pair2.Label] +
                   this.Pair[c.Match2.Pair1.Label] +
                   this.Pair[c.Match2.Pair2.Label];
        }

        // 指定組み合わせの出場回数をカウントアップ
        public void CountUp(MultiMatch c)
        {
            this.Match[c.Match1.Label]++;
            this.Match[c.Match2.Label]++;
#if false
            c.Match1.TriosSameCourt.Select(x => this.Trio[x.Label]++);
            c.Match2.TriosSameCourt.Select(x => this.Trio[x.Label]++);
#else
            this.Trio[c.Match1.TriosSameCourt.Skip(0).First().Label]++;
            this.Trio[c.Match1.TriosSameCourt.Skip(1).First().Label]++;
            this.Trio[c.Match1.TriosSameCourt.Skip(2).First().Label]++;
            this.Trio[c.Match1.TriosSameCourt.Skip(3).First().Label]++;
            this.Trio[c.Match2.TriosSameCourt.Skip(0).First().Label]++;
            this.Trio[c.Match2.TriosSameCourt.Skip(1).First().Label]++;
            this.Trio[c.Match2.TriosSameCourt.Skip(2).First().Label]++;
            this.Trio[c.Match2.TriosSameCourt.Skip(3).First().Label]++;
#endif
#if false
            c.Match1.PairsSameCourt.Select(x => this.courtPair[x.Label]++);
            c.Match2.PairsSameCourt.Select(x => this.courtPair[x.Label]++);
#else
            this.courtPair[c.Match1.PairsSameCourt.Skip(0).First().Label]++;
            this.courtPair[c.Match1.PairsSameCourt.Skip(1).First().Label]++;
            this.courtPair[c.Match1.PairsSameCourt.Skip(2).First().Label]++;
            this.courtPair[c.Match1.PairsSameCourt.Skip(3).First().Label]++;
            this.courtPair[c.Match1.PairsSameCourt.Skip(4).First().Label]++;
            this.courtPair[c.Match1.PairsSameCourt.Skip(5).First().Label]++;
            this.courtPair[c.Match2.PairsSameCourt.Skip(0).First().Label]++;
            this.courtPair[c.Match2.PairsSameCourt.Skip(1).First().Label]++;
            this.courtPair[c.Match2.PairsSameCourt.Skip(2).First().Label]++;
            this.courtPair[c.Match2.PairsSameCourt.Skip(3).First().Label]++;
            this.courtPair[c.Match2.PairsSameCourt.Skip(4).First().Label]++;
            this.courtPair[c.Match2.PairsSameCourt.Skip(5).First().Label]++;
#endif
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

        // 同じ3人がコート上に存在しているか
        public bool isExistTwoMore(MultiMatch c)
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
                var player = new Player(int.Parse(key));
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
    }
}
