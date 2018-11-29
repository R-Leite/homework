using System.Collections.Generic;
using System.Linq;

namespace TennisCompetition
{
    class Match
    {
        public readonly Pair Pair1;
        public readonly Pair Pair2;
        public readonly List<Pair> PairsSameCourt;
        public readonly List<Trio> TriosSameCourt;
        public readonly string Label;

        public Match(Pair p1, Pair p2)
        {
            this.Pair1 = p1;
            this.Pair2 = p2;
            var playerList = new List<Player>() { p1.Player1, p1.Player2, p2.Player1, p2.Player2 };
            Label = playerList.Select(x => x.Label).OrderBy(x => x).Select(x => x.ToString()).Aggregate((a, b) => a + "-" + b);

            // 同コート内のプレイヤー組み合わせ(2人)
            this.PairsSameCourt = new List<Pair>();
            for (var i = 0; i < 4; i++)
            {
                for (var j = i + 1; j < 4; j++)
                {
                    this.PairsSameCourt.Add(new Pair(playerList[i], playerList[j]));
                }
            }

            // 同コート内のプレイヤー組み合わせ(3人)
            this.TriosSameCourt = new List<Trio>();
            for (var i = 0; i < 4; i++)
            {
                for (var j = i + 1; j < 4; j++)
                {
                    for (var k = j + 1; k < 4; k++)
                    {
                        this.TriosSameCourt.Add(new Trio(playerList[i], playerList[j], playerList[k]));
                    }
                }
            }
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
