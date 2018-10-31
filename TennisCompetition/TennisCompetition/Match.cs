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

        public Match(Pair p1, Pair p2)
        {
            this.Pair1 = p1;
            this.Pair2 = p2;
        }

        public bool Contains(Match m)
        {
            if (this.Pair1.Contains(m.Pair1)) { return true; }
            if (this.Pair1.Contains(m.Pair2)) { return true; }

            if (this.Pair2.Contains(m.Pair1)) { return true; }
            if (this.Pair2.Contains(m.Pair2)) { return true; }

            return false;
        }

        public void ParticipateCount()
        {
            this.Pair1.ParticipateCount();
            this.Pair2.ParticipateCount();
        }

        public override string ToString()
        {
            return this.Pair1.ToString() + ", " + this.Pair2.ToString();
        }
    }
}
