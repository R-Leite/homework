using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Match
    {
        public readonly Pair Pair1;
        public readonly Pair Pair2;
        public readonly Pair Pair3;
        public readonly Pair Pair4;
        public int Participate;

        public Match(Pair p1, Pair p2, Pair p3, Pair p4)
        {
            this.Pair1 = p1;
            this.Pair2 = p2;
            this.Pair3 = p3;
            this.Pair4 = p4;
            this.Participate = 0;
        }

        public bool Contains(Match m)
        {
            if (this.Pair1 == m.Pair1) { return true; }
            if (this.Pair1 == m.Pair2) { return true; }
            if (this.Pair1 == m.Pair3) { return true; }
            if (this.Pair1 == m.Pair4) { return true; }

            if (this.Pair2 == m.Pair1) { return true; }
            if (this.Pair2 == m.Pair2) { return true; }
            if (this.Pair2 == m.Pair3) { return true; }
            if (this.Pair2 == m.Pair4) { return true; }

            if (this.Pair3 == m.Pair1) { return true; }
            if (this.Pair3 == m.Pair2) { return true; }
            if (this.Pair3 == m.Pair3) { return true; }
            if (this.Pair3 == m.Pair4) { return true; }

            if (this.Pair4 == m.Pair1) { return true; }
            if (this.Pair4 == m.Pair2) { return true; }
            if (this.Pair4 == m.Pair3) { return true; }
            if (this.Pair4 == m.Pair4) { return true; }

            return false;
        }

        public void ParticipateCount()
        {
            this.Participate++;
            this.Pair1.ParticipateCount();
            this.Pair2.ParticipateCount();
            this.Pair3.ParticipateCount();
            this.Pair4.ParticipateCount();
        }

        public override string ToString()
        {
            return this.Pair1.ToString() + ", " + this.Pair2.ToString() + ", " + this.Pair3.ToString() + ", " + this.Pair4.ToString();
        }
    }
}
