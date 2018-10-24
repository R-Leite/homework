using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Match
    {
        public Pair Pair1;
        public Pair Pair2;
        public Pair Pair3;
        public Pair Pair4;

        public Match(Pair p1, Pair p2, Pair p3, Pair p4)
        {
            Pair1 = p1;
            Pair2 = p2;
            Pair3 = p3;
            Pair4 = p4;
        }

        public bool contains(Match m)
        {
            if (Pair1 == m.Pair1) { return true; }
            if (Pair1 == m.Pair2) { return true; }
            if (Pair1 == m.Pair3) { return true; }
            if (Pair1 == m.Pair4) { return true; }

            if (Pair2 == m.Pair1) { return true; }
            if (Pair2 == m.Pair2) { return true; }
            if (Pair2 == m.Pair3) { return true; }
            if (Pair2 == m.Pair4) { return true; }

            if (Pair3 == m.Pair1) { return true; }
            if (Pair3 == m.Pair2) { return true; }
            if (Pair3 == m.Pair3) { return true; }
            if (Pair3 == m.Pair4) { return true; }

            if (Pair4 == m.Pair1) { return true; }
            if (Pair4 == m.Pair2) { return true; }
            if (Pair4 == m.Pair3) { return true; }
            if (Pair4 == m.Pair4) { return true; }

            return false;
        }
    }
}
