using System;
using System.Collections.Generic;
using System.Linq;


namespace TennisCompetition
{
    class Answer
    {
        const int MaxMatchCount = 25;

        private IEnumerable<MultiMatch> _twoMatches;
        private Participation _participation;

        public Answer(IEnumerable<MultiMatch> m, Participation p)
        {
            this._twoMatches = new List<MultiMatch>(m);
            this._participation = p;
        }

        public void Output()
        {
            var matchCount = 0;

            while (true)
            {
                // 出場回数から対戦組み合わせ決定
                //var matchCombination = this._twoMatches
                //    .Select(tc => new { Weight = this._participation.GetWeight(tc), tc })
                //    .Aggregate((min, next) => (min.Weight > next.Weight) ? next : min).tc;

                var matchCombination = this._twoMatches
                    .OrderBy(tm => this._participation.GetWeight(tm))
                    .First();

                // 出場回数をカウントアップ
                this._participation.CountUp(matchCombination);

                // 組み合わせを出力
                Console.WriteLine(matchCombination.ToString());
                Console.WriteLine(matchCombination.ToAnswer(this._participation));

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
