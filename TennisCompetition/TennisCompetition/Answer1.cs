using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Answer1
    {
        private List<Competition> _competitions;
        private Participation _participation;

        public Answer1(List<Competition> c, Participation p)
        {
            this._competitions = new List<Competition>(c);
            this._participation = p;
        }

        public void Output()
        {
            var competitionCount = this._competitions.Count;

            while (true)
            {
                var minWeight = int.MaxValue;
                var index = 0;

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

                var comp = this._competitions[index];

                // 全ペアが出場したら終了
                if (this._participation.isAllPair(comp))
                {
                    break;
                }
                this._participation.CountUp(comp);
                Console.WriteLine(comp.ToString());
                Console.WriteLine(comp.ToAnswer(this._participation));
            }

            this._participation.WriteLinePlayer();
            this._participation.WriteLinePair();
        }
    }
}
