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
        public double Height { get; }
        public double Width { get; }
        #endregion

        #region コンストラクタ
        public Rectangle(Point _drawOrigin, double _height, double _width) : base(_drawOrigin, _height * _width, "長方形")
        {
            this.Height = _height;
            this.Width = _width;
        }
        #endregion

        #region public メソッド
        public override void Draw()
        {
            Console.WriteLine($"({this.DrawOrigin.X}, {this.DrawOrigin.Y})に短辺{this.Height}, 長編{this.Width}, 面積{this.Area}の{this.Text}を描きます");
        }
        #endregion
    }
}
