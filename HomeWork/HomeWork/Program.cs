using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // 2人の名前の画数をCSVで入力してもらう
            // 2人分の画数を数値列に変更する
            // 数値の隣り合う値を加算して加算値の下一桁による新しい数値を作り、
            // 最終的に100以下の数値を作成する
            // 結果が二人の相性度（％）となる相性占いプログラムを作成する
            //while (true)
            //{
            //    Console.Write("ひとりめの名前の画数（カンマ区切り） => ");
            //    var s1 = Console.ReadLine();
            //    if (s1.Length == 0) break;
            //    Console.Write("ふたりめの名前の画数（カンマ区切り） => ");
            //    var s2 = Console.ReadLine();
            //    if (s2.Length == 0) break;
            //    var q = (s1 + "," + s2).Split(',');
            //    if (!q.All(x => int.TryParse(x, out int n)) || q.Any(x => int.Parse(x) < 0))
            //    {
            //        Console.WriteLine("不正な文字(1以上の自然数以外)が含まれています。");
            //        continue;
            //    }
            //    Console.WriteLine("ふたりの相性 = {0}%", HomeWork01(q.Select(x => int.Parse(x))));
            //}

            // 二項演算子ひとつと2つの数値（X = { x | x ∈ R，| x | > 0.1}）を入力すると正しく計算を行う関数を作成する
            // 演算は四則演算のみで、入力される演算子及び数値に誤りはなく、演算によるオーバーフローやアンダーフローは起こさないものとして良い
            // ただし、条件分岐及び計算式の文字列を入れると数値変換して計算してくれるもの（JavaScriptやLISPのeval系など）や
            // それに類するものは使用してはならない（可能な限りimmutableで）
            //while (true)
            //{
            //    Console.Write("演算子（+,-,*,/）と2つの実数（|x| > 0.1） => ");
            //    var s1 = Console.ReadLine();
            //    if (s1.Length == 0) break;
            //    var q = s1.Split(' ');
            //    if (q.Length < 3)
            //    {
            //        Console.WriteLine("入力が不正です → {0}", q);
            //        continue;
            //    }
            //    //    var operands = new List<string>() { "+", "-", "*", "/" };
            //    //    if (!operands.Contains(q[0]))
            //    //    {
            //    //        Console.WriteLine("演算子が不正です → {0}", q[0]);
            //    //        continue;
            //    //    }
            //    if (!double.TryParse(q[1], out double m) || Math.Abs(m) < 0.001)
            //    {
            //        Console.WriteLine("値が適当ではありません → {0}", q[1]);
            //        continue;
            //    }
            //    if (!double.TryParse(q[2], out double n) || Math.Abs(n) < 0.001)
            //    {
            //        Console.WriteLine("値が適当ではありません → {0}", q[2]);
            //        continue;
            //    }
            //    Console.WriteLine("{0} {1} {2} = {3}", q[1], q[0], q[2], HomeWork02(q[0], m, n));
            //}

            // テンキーの'5'を中心として各数字の方向にプレイヤーが移動するプログラムを作成する
            // オブジェクト指向プログラムとし、プレイヤークラス他、クラス設計を行う
            // マップは四隅が(0,0),(0,10),(10,10),(10,0)の2次元四辺形平面とし、プレイヤーの初期位置は(0,0)とする
            // プレイヤークラスのコンストラクタは初期位置を受け取るものとする
            // プレイヤーは境界を越えて移動することはできない
            // テンキーの'5'はその場に留まることを表す
            // 斜め方向に移動しようとして水平あるいは垂直方向に移動できない場合は、
            // 指定された移動可能な水平あるいは垂直方向のいずれかに移動する（マップの境界に平行に移動する）
            // プレイヤークラスは移動方向を受け取り移動後の位置を返す公開されたWalk（あるいはwalk）メソッドを持つ
            //var player = new Walker(0, 0);
            //Console.WriteLine("プレイヤー = {0}", player.Position);
            //while (true)
            //{
            //    Console.Write("方向を数字で入力 => ");
            //    var s = Console.ReadLine();
            //    if (!int.TryParse(s, out int n)) break;
            //    var newPos = player.Walk((Character.Direction)n);
            //    Console.WriteLine("プレイヤー = {0}", newPos);
            //}

            var cellPlayer = new CellPlayer(Cell.origin);
            Console.WriteLine("プレイヤー = {0}", Cell.CellNoMap[Cell.origin]);
            while (true)
            {
                Console.Write("方向を数字で入力 => ");
                var s = Console.ReadLine();
                if (!int.TryParse(s, out int n)) break;
                var newPos = cellPlayer.Walk((Cell.Direction)n);
                Console.WriteLine("プレイヤー = {0}", newPos);
            }
        }

        static int HomeWork01(IEnumerable<int> array)
        {
            Console.WriteLine(array.Select(x => x.ToString()).Aggregate((x, y) => x + "," + y));
            if (array.Count() < 4)
            {
                var n = array.Aggregate((x, y) => x * 10 + y);
                if (n <= 100) return n;
            }
            return HomeWork01(array.Zip(array.Skip(1), (first, second) => (first + second) % 10));
        }

        private static readonly Dictionary<string, Func<double, double, double>> calcOperandDic
            = new Dictionary<string, Func<double, double, double>>()
            {
                {"+", (a, b) => a + b},
                {"-", (a, b) => a - b},
                {"*", (a, b) => a * b},
                {"/", (a, b) => a / b}
            };

        static double HomeWork02(string operand, double x, double y)
        {
            try
            {
                return calcOperandDic[operand](x, y);
            }
            catch (System.Collections.Generic.KeyNotFoundException e)
            {
                throw new System.ArgumentException(e.Message);
            }
        }
    }
}
