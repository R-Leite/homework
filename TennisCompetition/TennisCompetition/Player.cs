using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Player
    {
        public int _playerNum;
        private int _appearance;

        public Player(int pn)
        {
            this._playerNum = pn;
            this._appearance = 0;
        }

        public void apperanceCountUp()
        {
            this._appearance++;
        }
    }
}
