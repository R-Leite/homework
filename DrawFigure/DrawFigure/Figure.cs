using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawFigure
{
    abstract class Figure
    {
        #region インスタンス変数
        public double Area { get; }
        public Point DrawOrigin { get; }
        public string Text { get; }
        #endregion

        public Figure(Point _drawOrigin, double _area, string _text)
        {
            this.DrawOrigin = _drawOrigin;
            this.Area = _area;
            this.Text = _text;
        }

        #region public メソッド
        public abstract void Draw();
        #endregion
    }
}
