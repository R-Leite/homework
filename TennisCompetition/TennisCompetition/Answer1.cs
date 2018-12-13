using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisCompetition
{
    class Answer1
    {
        private IEnumerable<MultiMatch> _twoMatches;
        private Participation _participation;

        public Answer1(IEnumerable<MultiMatch> m, Participation p)
        {
            this._twoMatches = new List<MultiMatch>(m);
            this._participation = p;
        }

        public void Output()
        {
            while (true)
            {
#if true
                // 出場回数から対戦組み合わせ決定
                var matchCombination = this._twoMatches
                    .OrderBy(tm => this._participation.GetWeightForAnswer1(tm))
                    .First();
#else
                var twoMatcesCount = _twoMatches.Count();
                var minWeight = int.MaxValue;
                var index = int.MaxValue;

                // 出場回数から優先順位をつける
                for (var i = 0; i < twoMatcesCount; i++)
                {
                    var weight = this._participation.GetWeightFor1(this._twoMatches.Skip(i).First());

                    if (minWeight > weight)
                    {
                        minWeight = weight;
                        index = i;
                    }
                }

                // 組み合わせ決定
                var matchCombination = this._twoMatches.Skip(index).First();
#endif


                // 出場回数をカウントアップ
                this._participation.CountUp(matchCombination);

                Console.WriteLine(matchCombination.ToString());
                Console.WriteLine(matchCombination.ToAnswer(this._participation));

                // 全ペアが出場したら終了
                if (this._participation.isAllPairAtLeastOnce())
                {
                    break;
                }
            }

            // 各プレイヤーの出場回数を表示
            this._participation.WriteLinePlayer();

            // 各ペアの出場回数を表示
            this._participation.WriteLinePair();
        }
    }
}
