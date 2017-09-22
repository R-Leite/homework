using System;
using System.Collections.Generic;

namespace HomeWork
{
    abstract class Character
    {
        public enum Direction
        {
            DOWN_LEFT	= 1,
            DOWN				= 2,
            DOWN_RIGHT = 3,
            LEFT				= 4,
            STAY				= 5,
            RIGHT				= 6,
            UP_LEFT			= 7,
            UP					= 8,
            UP_RIGHT		= 9
        }

        protected static Point Move(int x, int y)
        {
            return new Point(
                Math.Max(Map.X_MINIMUM, Math.Min(Map.X_MAXIMUM, x)),
                Math.Max(Map.Y_MINIMUM, Math.Min(Map.Y_MAXIMUM, y))
            );
        }

        protected static Point Move(Point pt)
        {
            return Move(pt.x, pt.y);
        }

        protected static bool CanMove(Point pt)
        {
            if (pt.x >= Map.X_MINIMUM && pt.x <= Map.X_MAXIMUM
                && pt.y >= Map.Y_MINIMUM && pt.y <= Map.Y_MAXIMUM)
                return true;
            else
                return false;
        }
    }

    class Walker : Character
    {
        private static readonly Dictionary<Direction, Func<Point, Point>> moveDic =
            new Dictionary<Direction, Func<Point, Point>>()
        {
            {Direction.DOWN_LEFT,		p => Move(p + new Point(-1, -1))},
            {Direction.DOWN, 		    	p => Move(p + new Point(0, -1))},
            {Direction.DOWN_RIGHT,	p => Move(p + new Point(1, -1))},
            {Direction.LEFT,       			p => Move(p + new Point(-1, 0))},
            {Direction.STAY,       			p => Move(p + new Point(0, 0))},
            {Direction.RIGHT,      			p => Move(p + new Point(1, 0))},
            {Direction.UP_LEFT,    		p => Move(p + new Point(-1, 1))},
            {Direction.UP,         			p => Move(p + new Point(0, 1))},
            {Direction.UP_RIGHT,   		p => Move(p + new Point(1, 1))}
        };

        public Point Position { get; private set; }

        public Walker(int x, int y)
        {
            Position = new Point(x, y);
        }

        public Point Walk(Direction dir)
        {
            return Enum.IsDefined(typeof(Direction), dir) ?
                Position = moveDic[dir](Position) : Position;
        }
    }
}
