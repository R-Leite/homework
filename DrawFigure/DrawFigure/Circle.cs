using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawFigure
{
    class Circle : Figure
    {
        #region インスタンス変数
        public double Radius {get;}
        #endregion

        public Circle(Point _drawOrigin, double _radius) : base(_drawOrigin, _radius * _radius * Math.PI, "円")
        {
            this.Radius =  _radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"({this.DrawOrigin.X}, {this.DrawOrigin.Y})に半径{this.Radius}, 面積{this.Area}の{this.Text}を描きます");
        }
    }
}
