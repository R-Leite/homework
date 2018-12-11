using System.Collections.Generic;
using System.Linq;

namespace TennisCompetition
{
    // 1面分の試合を管理するクラス
    class Match
    {
        public readonly Pair Pair1;
        public readonly Pair Pair2;
        public readonly IEnumerable<Pair> PairsSameCourt;
        public readonly IEnumerable<Trio> TriosSameCourt;
        public readonly string Label;

        public Match(Pair pair1, Pair pair2)
        {
            this.Pair1 = pair1;
            this.Pair2 = pair2;
            var playerList = new List<Player>() { pair1.Player1, pair1.Player2, pair2.Player1, pair2.Player2 };
            this.Label = playerList.Select(x => x.Label).OrderBy(x => x).Select(x => x.ToString()).Aggregate((a, b) => a + "-" + b);

            // 同コート内のプレイヤー組み合わせ(2人)
            this.PairsSameCourt = playerList.SelectMany((x, idx) =>
            playerList.Skip(idx + 1).Select(y => new Pair(x, y)));

            // 同コート内のプレイヤー組み合わせ(3人)
            this.TriosSameCourt = playerList.SelectMany((x, idx) =>
            playerList.Skip(idx + 1).SelectMany((y, idy) =>
            playerList.Skip(idx + idy + 2).Select(z => new Trio(x, y, z))));
        }

        public bool Contains(Match m)
        {
            if (this.Pair1.Contains(m.Pair1)) { return true; }
            if (this.Pair1.Contains(m.Pair2)) { return true; }
            if (this.Pair2.Contains(m.Pair1)) { return true; }
            if (this.Pair2.Contains(m.Pair2)) { return true; }

            return false;
        }

        public override string ToString()
        {
            return this.Pair1.ToString() + ", " + this.Pair2.ToString();
        }
    }
}
