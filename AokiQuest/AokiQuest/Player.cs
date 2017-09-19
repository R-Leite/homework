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
        Point point;
        private int _x;
        private int _y;
        private string _icon = "^";
        private Dictionary<string, Point> moveMap = new Dictionary<string, Point>
        {
            {"7", new Point(-1, 1) }
        };

        public Player(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Walk(int direction)
        {
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
