using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Competition
    {
        public readonly Match Match1;
        public readonly Match Match2;

        public Competition(Match m1, Match m2)
        {
            this.Match1 = m1;
            this.Match2 = m2;
        }

        public override string ToString()
        {
            return this.Match1.ToString() + ", " + this.Match2.ToString();
        }
    }
}
