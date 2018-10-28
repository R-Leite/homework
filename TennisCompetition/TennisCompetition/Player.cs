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
        private int _participate;

        public Player(int pn)
        {
            this._playerNum = pn;
            this._participate = 0;
        }

        public void participateCount()
        {
            this._participate++;
        }
    }
}
