using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawFigure
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle1 = new Circle(new Point(10, 10), 5.0);
            var circle2 = new Circle(new Point(15, 20), 10.0);
            var rectangle1 = new Rectangle(new Point(15, 20), 3.3, 6.6);
            var rectangle2 = new Rectangle(new Point(10, 20), 5.3, 11.6);
            var square1 = new Square(new Point(50, 20), 30.0);
            var square2 = new Square(new Point(50, 50), 10.0);

            // 図形をコレクションに登録
            var figureList = new List<Figure>() {circle1, circle2, rectangle1, rectangle2, square1, square2};

            // 各々の図形を描く
            figureList.ForEach(_ => _.Draw());

            Console.ReadLine();
        }
    }
}
