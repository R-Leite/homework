namespace HomeWork
{
    public struct Point
    {
        public readonly int x;
        public readonly int y;

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public override string ToString()
        {
            return "(" + x + "," + y + ")";
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }
    }

    static class Map
    {
        public const int X_MINIMUM = 0;
        public const int X_MAXIMUM = 10;
        public const int Y_MINIMUM = 0;
        public const int Y_MAXIMUM = 10;
    }
}
