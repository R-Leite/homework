using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Player
    {
        public readonly int Label;

        public Player(int pn)
        {
            this.Label = pn;
        }

        public override string ToString()
        {
            return this.Label.ToString().PadLeft(2, ' ');
        }
    }
}
