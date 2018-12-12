using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisCompetition
{
    class Answer1
    {
        private IEnumerable<MultiMatch> _twoCourtsList;
        private Participation _participation;

        public Answer1(IEnumerable<MultiMatch> t, Participation p)
        {
            this._twoCourtsList = new List<MultiMatch>(t);
            this._participation = p;
        }

        public void Output()
        {
            var twoCourtsCount = this._twoCourtsList.Count();

            while (true)
            {
#if True
                // 出場回数から対戦組み合わせ決定
                var twoCourts = this._twoCourtsList
                    .Select(tc => new { Weight = this._participation.GetWeightFor1(tc), tc })
                    .Aggregate((min, next) => (min.Weight > next.Weight) ? next : min).tc;
#else
                var minWeight = int.MaxValue;
                var index = int.MaxValue;

                // 出場回数から優先順位をつける
                for (var i = 0; i < twoCourtsCount; i++)
                {
                    var weight = this._participation.GetWeightFor1(this._twoCourts[i]);

                    if (minWeight > weight)
                    {
                        minWeight = weight;
                        index = i;
                    }
                }

                // 組み合わせ決定
                var twoCourts = this._twoCourts[index];
#endif

                // 出場回数をカウントアップ
                this._participation.CountUp(twoCourts);

                Console.WriteLine(twoCourts.ToString());
                Console.WriteLine(twoCourts.ToAnswer(this._participation));

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
