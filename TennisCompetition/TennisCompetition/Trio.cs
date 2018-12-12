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

        public Trio(Player p1, Player p2, Player p3)
        {
            this.Player1 = p1;
            this.Player2 = p2;
            this.Player3 = p3;
        }

        public override string ToString()
        {
            return "(" + this.Player1.ToString() + "," + this.Player2.ToString() + "," + this.Player3.ToString() + ")";
        }
    }
}
