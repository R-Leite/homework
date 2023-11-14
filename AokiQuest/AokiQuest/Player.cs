using System;
using System.Collections.Generic;

namespace AokiQuest
{
    public class Player
    {
        #region 定数
        public static readonly Point origin = new Point(0, 0);
        #endregion

        #region Direction
        public enum Direction
        {
            DOWN_LEFT  = 1,
            DOWN       = 2,
            DOWN_LIGHT = 3,
            LEFT       = 4,
            STAY       = 5,
            RIGHT      = 6,
            UP_LEFT    = 7,
            UP         = 8,
            UP_RIGHT   = 9
        }
        #endregion

        #region インスタンス変数
        private Point _point;

        private Map _map;

        private Dictionary<Direction, Point> _moveMap = new Dictionary<Direction, Point>
        {
            { Direction.DOWN_LEFT , new Point(-1, -1) },
            { Direction.DOWN      , new Point( 0, -1) },
            { Direction.DOWN_LIGHT, new Point( 1, -1) },
            { Direction.LEFT      , new Point(-1,  0) },
            { Direction.STAY      , new Point( 0,  0) },
            { Direction.RIGHT     , new Point( 1,  0) },
            { Direction.UP_LEFT   , new Point(-1,  1) },
            { Direction.UP        , new Point( 0,  1) },
            { Direction.UP_RIGHT  , new Point( 1,  1) }
        };
        #endregion

        #region コンストラクタ
        public Player(int x, int y)
        {
            _point = new Point(x, y);
            _map = new Map();
        }
        #endregion

        // 1マス、ななめなら√2移動
        public Point Walk(Direction direction)
        {
            _point = CorrectPoint(_point + (_moveMap.TryGetValueEx(direction, new Point(0, 0))));

            return _point;
        }

        // 現在位置がマップ外の場合マップ内に戻す
        private Point CorrectPoint(Point pt)
        {
            return new Point(Math.Max(_map.MinPoint.X, Math.Min(_map.MaxPoint.X, pt.X)),
                             Math.Max(_map.MinPoint.Y, Math.Min(_map.MaxPoint.Y, pt.Y)));
        }
    }
}
