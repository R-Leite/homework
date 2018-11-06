using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Participation
    {
        public Dictionary<Player, int> Player;
        public Dictionary<Pair, int> Pair;
        public Dictionary<string, int> Trio;
        public Dictionary<string, int> Match;

        public Participation(List<Player> players, List<Pair> pairs, List<Trio> trios, List<Match> matches)
        {
            this.Player = new Dictionary<Player, int>();
            this.Pair = new Dictionary<Pair, int>();
            this.Trio = new Dictionary<string, int>();
            this.Match = new Dictionary<string, int>();
            players.ForEach(p => this.Player.Add(p, 0));
            pairs.ForEach(p => this.Pair.Add(p, 0));
            trios.ForEach(t => this.Trio.Add(t.Group, 0));
            foreach(var m in matches)
            {
//                Console.WriteLine(m.ToString());
                if (!this.Match.ContainsKey(m.Group))
                {
                    this.Match.Add(m.Group, 0);
                }
            }
        }

        public int GetWeight(Competition c)
        {
            return this.Match[c.Match1.Group] +
                this.Match[c.Match2.Group] +
                //this.Trio[c.Match1.Trios[0].Group] +
                //this.Trio[c.Match1.Trios[1].Group] +
                //this.Trio[c.Match1.Trios[2].Group] +
                //this.Trio[c.Match1.Trios[3].Group] +
                //this.Trio[c.Match2.Trios[0].Group] +
                //this.Trio[c.Match2.Trios[1].Group] +
                //this.Trio[c.Match2.Trios[2].Group] +
                //this.Trio[c.Match2.Trios[3].Group] +
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

        public void Add(Competition c)
        {
            this.Match[c.Match1.Group]++;
            this.Match[c.Match2.Group]++;
            this.Trio[c.Match1.Trios[0].Group]++;
            this.Trio[c.Match1.Trios[1].Group]++;
            this.Trio[c.Match1.Trios[2].Group]++;
            this.Trio[c.Match1.Trios[3].Group]++;
            this.Trio[c.Match2.Trios[0].Group]++;
            this.Trio[c.Match2.Trios[1].Group]++;
            this.Trio[c.Match2.Trios[2].Group]++;
            this.Trio[c.Match2.Trios[3].Group]++;
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

        public bool isAllPair(Competition c)
        {
            if (this.Pair[c.Match1.Pair1] <= 0) { return false; }
            if (this.Pair[c.Match1.Pair2] <= 0) { return false; }
            if (this.Pair[c.Match2.Pair1] <= 0) { return false; }
            if (this.Pair[c.Match2.Pair2] <= 0) { return false; }
            return true;
        }

        public void show()
        {
            //foreach(var d in this.Match.Keys)
            //{
            //    var str = d[0].ToString() + ", " + d[1].ToString() + ", " + d[2].ToString() + ", " + d[3].ToString();
            //    Console.WriteLine($"{str}:{this.Match[d]}");
            //}
        }
    }
}
