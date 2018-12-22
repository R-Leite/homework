using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makeTen
{
    class NumberCombination
    {
        private readonly int num1;
        private readonly int num2;
        private readonly int num3;
        private readonly int num4;
        private Dictionary<string, Func<double, double, double>> _calcDict = new Dictionary<string, Func<double, double, double>>
        {
            {"+", (x, y) => x + y},
            {"-", (x, y) => x - y},
            {"*", (x, y) => x * y},
            {"/", (x, y) => x / y}
        };

        public NumberCombination(int n1, int n2, int n3, int n4)
        {
            this.num1 = n1;
            this.num2 = n2;
            this.num3 = n3;
            this.num4 = n4;
        }

        public override string ToString()
        {
            return "(" + this.num1 + ", " + this.num2 + ", " + this.num3 + ", " + this.num4 + ")";
        }

        // 同じ数字がある？
        public bool ContainsSameNumber()
        {
            if (this.num1 == this.num2) { return true; }
            if (this.num1 == this.num3) { return true; }
            if (this.num1 == this.num4) { return true; }
            if (this.num2 == this.num3) { return true; }
            if (this.num2 == this.num4) { return true; }
            if (this.num3 == this.num4) { return true; }

            return false;
        }

        public bool isMakeTen()
        {
            var operatorList = new List<string>() { "+", "-", "*", "/" };
            var answerOperators = operatorList.SelectMany(_ => operatorList, (x, y) => new { x, y })
            .SelectMany(_ => operatorList, (_, z) => new { _.x, _.y, z })
            .Where(_ => this.Calculate(_.z, this.Calculate(_.y, this.Calculate(_.x, this.num1, this.num2), this.num3), this.num4) == 10.0);

            foreach (var ans in answerOperators)
            {
                Console.WriteLine(this.num1 + ans.x + this.num2 + ans.y + this.num3 + ans.z + this.num4);
            }

            return true;
        }

        // 四則演算の計算結果を返す
        private double Calculate(string operater, double a, double b)
        {
            return _calcDict[operater](a, b);
        }
    }
}
