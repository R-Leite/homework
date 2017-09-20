using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AokiQuest
{
    public class Point
    {
        #region プロパティ
        public int X { get; set; }
        public int Y { get; set; }
        #endregion

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Add(Point p)
        {
            X += p.X;
            Y += p.Y;
        }
    }
}
