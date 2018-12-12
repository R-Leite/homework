using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    // ペアを管理するクラス
    class Pair
    {
        public readonly Player Player1;
        public readonly Player Player2;

        public Pair(Player p1, Player p2)
        {
            this.Player1 = p1;
            this.Player2 = p2;
        }

        public bool Contains(Pair p)
        {
            if (this.Player1.Equals(p.Player1)) { return true; }
            if (this.Player1.Equals(p.Player2)) { return true; }
            if (this.Player2.Equals(p.Player1)) { return true; }
            if (this.Player2.Equals(p.Player2)) { return true; }
            return false;
        }

        override public String ToString()
        {
            return "(" + this.Player1.ToString() + "," + this.Player2.ToString() + ")";
        }
    }
}
