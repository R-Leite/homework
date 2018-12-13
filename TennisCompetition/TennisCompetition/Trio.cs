using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Trio
    {
        private Player _player1;
        private Player _player2;
        private Player _player3;
        public readonly string Label;

        public Trio(Player p1, Player p2, Player p3)
        {
            this._player1 = p1;
            this._player2 = p2;
            this._player3 = p3;
            this.Label = new List<Player>() { p1, p2, p3 }.Select(x => x.Label).OrderBy(x => x).Select(x => x.ToString()).Aggregate((a, b) => a + "-" + b);
        }

        public override string ToString()
        {
            return "(" + this._player1.ToString() + "," + this._player2.ToString() + "," + this._player3.ToString() + ")";
        }
    }
}
