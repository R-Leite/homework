using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    // プレイヤーを管理するクラス
    class Player
    {
        public readonly string Label;

        public Player(int pn)
        {
            this.Label = pn.ToString();
        }

        public override string ToString()
        {
            return this.Label.PadLeft(2, ' ');
        }

        public bool Equals(Player p)
        {
            return this.Label == p.Label;
        }
    }
}
