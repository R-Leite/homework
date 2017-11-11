using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q6
{
    class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double getDistance(Point p)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(X - p.X), 2) + Math.Pow(Math.Abs(Y - p.Y), 2));
        }
    }
}
