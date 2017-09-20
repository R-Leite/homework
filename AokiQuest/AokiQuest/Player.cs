using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AokiQuest
{
    public class Player
    {
        public readonly int hoge;
        public readonly Point _point;
        private Dictionary<string, Point> _moveMap = new Dictionary<string, Point>
        {
            { "7", new Point(-1, 1) }, { "8", new Point(0, 1) }, { "9", new Point(1, 1) },
            { "4", new Point(-1, 0) }, { "5", new Point(0, 0) }, { "6", new Point(1, 0) },
            { "1", new Point(-1, -1) }, { "2", new Point(0, -1) }, { "3", new Point(1, -1) }
        };

        public Player(int x, int y)
        {
            _point = new Point(x, y);
        }

        public void Walk(string direction)
        {
            _point.Add(_moveMap[direction]);
        }
    }
}
