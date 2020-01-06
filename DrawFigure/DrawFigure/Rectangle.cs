using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawFigure
{
    class Rectangle : Figure
    {
        #region インスタンス変数
        public double ShortSide {get;}
        public double LongSide { get; }
        #endregion

        #region コンストラクタ
        public Rectangle(Point _drawOrigin, double _shortSide, double _longSide) : base(_drawOrigin, _shortSide * _longSide, "長方形")
        {
            this.ShortSide = _shortSide;
            this.LongSide = _longSide;
        }
        #endregion

        #region public メソッド
        public override void Draw()
        {
            Console.WriteLine($"({this.DrawOrigin.X}, {this.DrawOrigin.Y})に短辺{this.ShortSide}, 長編{this.LongSide}, 面積{this.Area}の{this.Text}を描きます");
        }
        #endregion
    }
}
