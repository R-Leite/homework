using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Trio
    {
        public readonly Player Player1;
        public readonly Player Player2;
        public readonly Player Player3;
        public readonly string Group;

        public Trio(Player p1, Player p2, Player p3)
        {
            this.Player1 = p1;
            this.Player2 = p2;
            this.Player3 = p3;
            this.Group = new List<Player>() { p1, p2, p3 }.Select(x => x.Label).OrderBy(x => x).Select(x => x.ToString()).Aggregate((a, b) => a + "-" + b);
        }

        public bool Contains(Trio t)
        {
            if (this.Player1 == t.Player1) { return true; }
            if (this.Player1 == t.Player2) { return true; }
            if (this.Player1 == t.Player3) { return true; }
            if (this.Player2 == t.Player1) { return true; }
            if (this.Player2 == t.Player2) { return true; }
            if (this.Player2 == t.Player3) { return true; }
            if (this.Player3 == t.Player1) { return true; }
            if (this.Player3 == t.Player2) { return true; }
            if (this.Player3 == t.Player3) { return true; }

            return false;
        }

        public override string ToString()
        {
            return "(" + Player1.ToString() + ", " + Player2.ToString() + ", " + Player3.ToString() + ")";
        }
    }
}
