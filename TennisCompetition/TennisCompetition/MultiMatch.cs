using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    // 2面分の試合を管理するクラス
    class MultiMatch
    {
        public readonly Match Match1;
        public readonly Match Match2;

        public MultiMatch(Match m1, Match m2)
        {
            this.Match1 = m1;
            this.Match2 = m2;
        }

        public override string ToString()
        {
            return this.Match1.ToString() + ", " + this.Match2.ToString();
        }

        // 出場回数文字列(答え表示用)
        public string ToAnswer(Participation participation)
        {
            return
                this.Match1.Pair1.Player1.Label + "," +
                this.Match1.Pair1.Player2.Label + "," +
                this.Match1.Pair2.Player1.Label + "," +
                this.Match1.Pair2.Player2.Label + "," +
                this.Match2.Pair1.Player1.Label + "," +
                this.Match2.Pair1.Player2.Label + "," +
                this.Match2.Pair2.Player1.Label + "," +
                this.Match2.Pair2.Player2.Label + ":" +
                //
                participation.Player[this.Match1.Pair1.Player1.Label] + "," +
                participation.Player[this.Match1.Pair1.Player2.Label] + "," +
                participation.Player[this.Match1.Pair2.Player1.Label] + "," +
                participation.Player[this.Match1.Pair2.Player2.Label] + "," +
                participation.Player[this.Match2.Pair1.Player1.Label] + "," +
                participation.Player[this.Match2.Pair1.Player2.Label] + "," +
                participation.Player[this.Match2.Pair2.Player1.Label] + "," +
                participation.Player[this.Match2.Pair2.Player2.Label];
        }
    }
}
