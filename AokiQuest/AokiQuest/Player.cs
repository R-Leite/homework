using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AokiQuest
{
    public class Player
    {
        private int _x;
        private int _y;
        private string _icon = "^";
        private Dictionary<string, int> move;

        public Player(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Walk(int direction)
        {
            switch(direction)
            {
                case 2:
                        _y++;
                        break;
                case 4:
                    _x--;
                    break;
                case 6:
                    _x++;
                    break;
                case 8:
                    _y--;
                    break;
                default:
                    break;
            }
        }
    }
}
