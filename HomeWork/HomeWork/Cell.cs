using System;
using System.Collections.Generic;

namespace HomeWork
{
    static class Cell
    {
        public enum Direction
        {
            DOWN_LEFT	= 1,
            DOWN				= 2,
            DOWN_RIGHT	= 3,
            LEFT				= 4,
            STAY				= 5,
            RIGHT				= 6,
            UP_LEFT			= 7,
            UP					= 8,
            UP_RIGHT		= 9
        }
        private const int _cellColumn = 11;
        private const int _cellRow = 11;
        private const int _columnDiff = 100;
        private const int _rowDiff = 1;
        public static readonly int origin = _rowDiff + _columnDiff;
        public static readonly Dictionary<int, String> CellNoMap;
        public static readonly Dictionary<Cell.Direction, int> CellNoDifference;
        public static readonly int[] NearCellDifference;

        static Cell()
        {
            CellNoMap = new Dictionary<int, string>();
            for (int i = 0; i < _cellRow; i++)
            {
                for (int j = 0; j < _cellColumn; j++)
                {
                    var point = "(" + j.ToString() + "," + i.ToString() + ")";
                    CellNoMap.Add((i + 1) * _columnDiff + (j + 1) * _rowDiff, point);
                }
            }
            CellNoDifference = new Dictionary<Direction, int>()
            {
                { Direction.DOWN_LEFT,	-_columnDiff - _rowDiff},
                { Direction.DOWN,       		-_columnDiff},
                { Direction.DOWN_RIGHT,	-_columnDiff + _rowDiff},
                { Direction.LEFT,       			-_rowDiff},
                { Direction.STAY,       			0},
                { Direction.RIGHT,      		_rowDiff},
                { Direction.UP_LEFT,    		_columnDiff - _rowDiff},
                { Direction.UP,         			_columnDiff},
                { Direction.UP_RIGHT,   		_columnDiff + _rowDiff}
            };
            NearCellDifference = new int[] { -_columnDiff, -_rowDiff, _rowDiff, _columnDiff };
        }
    }

    class CellPlayer
    {
        private int _currentCellNo;
        public CellPlayer(int cellNo)
        {
            _currentCellNo = cellNo;
        }

        public String Walk(Cell.Direction dir)
        {
            _currentCellNo = GetNewCell(dir);
            return Cell.CellNoMap[_currentCellNo];
        }

        private int GetNewCell(Cell.Direction dir)
        {
            if (!Enum.IsDefined(typeof(Cell.Direction), dir)) return _currentCellNo;
            var newCellNo = _currentCellNo + Cell.CellNoDifference[dir];
            if (Cell.CellNoMap.ContainsKey(newCellNo)) return newCellNo;
            foreach(var diff in Cell.NearCellDifference)
            {
                var nearCellNo = newCellNo + diff;
                if (Cell.CellNoMap.ContainsKey(nearCellNo)) return nearCellNo;
            }
            return _currentCellNo;
        }
    }
}
