using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawFigure
{
    class Square : Figure
    {
        #region インスタンス変数
        public double Side {get;}
        #endregion

        public Square(Point _drawOrigin, double _side) : base(_drawOrigin, _side*_side, "正方形")
        {
            this.Side = _side;
        }

        public override void Draw()
        {
            Console.WriteLine($"({this.DrawOrigin.X}, {this.DrawOrigin.Y})に辺{this.Side}, 面積{this.Area}の{this.Text}を描きます");
        }
    }
}
