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
        //public int Participate;

        public Pair(Player p1, Player p2)
        {
            this.Player1 = p1;
            this.Player2 = p2;
            //this.Participate = 0;
        }

        public bool Contains(Pair p)
        {
            if (this.Player1 == p.Player1) { return true; }
            if (this.Player1 == p.Player2) { return true; }
            if (this.Player2 == p.Player1) { return true; }
            if (this.Player2 == p.Player2) { return true; }
            return false;
        }

        //public void ParticipateCount()
        //{
        //    this.Participate++;
        //    this.Player1.participateCount();
        //    this.Player2.participateCount();
        //}

        override public String ToString()
        {
            return "(" + this.Player1.ToString() + "," + this.Player2.ToString() + ")";
        }
    }
}
