using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Pair
    {
        public readonly Player Player1;
        public readonly Player Player2;
        public readonly string Label;
        //public int Participate;

        public Pair(Player p1, Player p2)
        {
            this.Player1 = p1;
            this.Player2 = p2;
            this.Label = p1.Label + "-" + p2.Label;
        }

        public bool Contains(Pair p)
        {
            if (this.Player1 == p.Player1) { return true; }
            if (this.Player1 == p.Player2) { return true; }
            if (this.Player2 == p.Player1) { return true; }
            if (this.Player2 == p.Player2) { return true; }
            return false;
        }

        override public String ToString()
        {
            return "(" + this.Player1.ToString() + "," + this.Player2.ToString() + ")";
        }
    }
}
