using System;
using System.Collections.Generic;
using System.Linq;


namespace TennisCompetition
{
    class Answer
    {
        const int MaxMatchCount = 25;

        private IEnumerable<MultiMatch> _twoCourtsList;
        private Participation _participation;

        public Answer(IEnumerable<MultiMatch> t, Participation p)
        {
            this._twoCourtsList = new List<MultiMatch>(t);
            this._participation = p;
        }

        public void Output()
        {
            var matchCount = 0;

            while (true)
            {
                // 出場回数から対戦組み合わせ決定
                var twoCourts = this._twoCourtsList
                    .Select(tc => new { Weight = this._participation.GetWeight(tc), tc })
                    .Aggregate((min, next) => (min.Weight > next.Weight) ? next : min).tc;

                // 出場回数をカウントアップ
                this._participation.CountUp(twoCourts);

                // 組み合わせを出力
                Console.WriteLine(twoCourts.ToString());
                Console.WriteLine(twoCourts.ToAnswer(this._participation));

                // 試合回数が指定数以上なら終了
                if (++matchCount >= MaxMatchCount)
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
