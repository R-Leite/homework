﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Answer1
    {
        private List<TwoCourts> _competitions;
        private Participation _participation;

        public Answer1(List<TwoCourts> c, Participation p)
        {
            this._competitions = new List<TwoCourts>(c);
            this._participation = p;
        }

        public void Output()
        {
            var competitionCount = this._competitions.Count;

            while (true)
            {
                var minWeight = int.MaxValue;
                var index = int.MaxValue;

                // 出場回数から優先順位をつける
                for (var i = 0; i < competitionCount; i++)
                {
                    var weight = this._participation.GetWeightFor1(this._competitions[i]);

                    if (minWeight > weight)
                    {
                        minWeight = weight;
                        index = i;
                    }
                }

                // 組み合わせ決定
                var comp = this._competitions[index];

                // 全ペアが出場したら終了
                if (this._participation.isAllPairAtLeastOnce())
                {
                    break;
                }

                // 出場回数をカウントアップ
                this._participation.CountUp(comp);

                Console.WriteLine(comp.ToString());
            }

            // 各プレイヤーの出場回数を表示
            this._participation.WriteLinePlayer();

            // 各ペアの出場回数を表示
            this._participation.WriteLinePair();
        }
    }
}
