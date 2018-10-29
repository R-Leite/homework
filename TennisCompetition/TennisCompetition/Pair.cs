using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Pair
    {
        public Player _player1;
        public Player _player2;
        public int _participate;

        public Pair(Player p1, Player p2)
        {
            _player1 = p1;
            _player2 = p2;
            this._participate = 0;
        }

        public bool contains(Pair p)
        {
            if (_player1 == p._player1) { return true; }
            if (_player1 == p._player2) { return true; }
            if (_player2 == p._player1) { return true; }
            if (_player2 == p._player2) { return true; }
            return false;
        }

        public void participateCount()
        {
            this._participate++;
        }
    }
}
