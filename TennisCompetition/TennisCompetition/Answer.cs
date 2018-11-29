using System;
using System.Collections.Generic;

namespace TennisCompetition
{
    class Answer
    {
        private List<TwoCourts> _competitions;
        private Participation _participation;

        public Answer(List<TwoCourts> c, Participation p)
        {
            this._competitions = new List<TwoCourts>(c);
            this._participation = p;
        }

        public void Output()
        {
            var competitionCount = this._competitions.Count;
            var matchCount = 0;

            while (true)
            {
                var minWeight = int.MaxValue;
                var index = int.MaxValue;

                // 出場回数から優先順位をつける
                for (var i = 0; i < competitionCount; i++)
                {
                    var weight = this._participation.GetWeight(this._competitions[i]);

                    if (minWeight > weight)
                    {
                        minWeight = weight;
                        index = i;
                    }
                }

                // 組み合わせ決定
                var comp = this._competitions[index];

                // 出場回数をカウントアップ
                this._participation.CountUp(comp);

                Console.WriteLine(comp.ToString());

                // 試合回数が25回以上なら終了
                if (++matchCount >= 25) { break; }
            }

            // 各プレイヤーの出場回数を表示
            this._participation.WriteLinePlayer();

            // 各ペアの出場回数を表示
            this._participation.WriteLinePair();
        }
    }
}
