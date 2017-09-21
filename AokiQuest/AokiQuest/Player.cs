using System.Collections.Generic;

namespace AokiQuest
{
    public class Player
    {
        #region インスタンス変数
        private Point _point;

        private Map _map;

        private Dictionary<int, Point> _moveMap = new Dictionary<int, Point>
        {
            { 1, new Point(-1, -1) },
            { 2, new Point( 0, -1) },
            { 3, new Point( 1, -1) },
            { 4, new Point(-1,  0) },
            { 6, new Point( 1,  0) },
            { 7, new Point(-1,  1) },
            { 8, new Point( 0,  1) },
            { 9, new Point( 1,  1) }
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
        public Point Walk(int direction)
        {
            if (_moveMap.ContainsKey(direction))
            {
                _point.Add(_moveMap[direction]);
            }

            CorrectPoint();

            return _point;
        }

        // 現在位置がマップ外の場合マップ内に戻す
        private void CorrectPoint()
        {
            if (_point.X < _map.MinPoint.X) { _point.X = _map.MinPoint.X; }
            if (_point.Y < _map.MinPoint.Y) { _point.Y = _map.MinPoint.Y; }
            if (_point.X > _map.MaxPoint.X) { _point.X = _map.MaxPoint.X; }
            if (_point.Y > _map.MaxPoint.Y) { _point.Y = _map.MaxPoint.Y; }
        }
    }
}
