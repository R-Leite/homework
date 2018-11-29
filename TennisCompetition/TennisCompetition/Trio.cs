using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Trio
    {
        public readonly string Label;

        public Trio(Player p1, Player p2, Player p3)
        {
            this.Label = new List<Player>() { p1, p2, p3 }.Select(x => x.Label).OrderBy(x => x).Select(x => x.ToString()).Aggregate((a, b) => a + "-" + b);
        }

        public override string ToString()
        {
            return Label;
        }
    }
}
