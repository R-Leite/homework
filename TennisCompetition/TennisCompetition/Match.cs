﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Match
    {
        public readonly Pair Pair1;
        public readonly Pair Pair2;
        public readonly string Group;
        public readonly List<Trio> Trios;

        public Match(Pair p1, Pair p2)
        {
            this.Pair1 = p1;
            this.Pair2 = p2;
            var list = new List<Player>() { p1.Player1, p1.Player2, p2.Player1, p2.Player2 };
            Group = list.Select(x=>x.Label.ToString()).Aggregate((a, b) => a + "-" + b);
            this.Trios = new List<Trio>();
            for (var i = 0; i < 4; i++)
            {
                for (var j = i + 1; j < 4; j++)
                {
                    for (var k = j + 1; k < 4; k++)
                    {
                        this.Trios.Add(new TennisCompetition.Trio(list[i], list[j], list[k]));
                    }
                }
            }
        }

        public bool Contains(Match m)
        {
            if (this.Pair1.Contains(m.Pair1)) { return true; }
            if (this.Pair1.Contains(m.Pair2)) { return true; }

            if (this.Pair2.Contains(m.Pair1)) { return true; }
            if (this.Pair2.Contains(m.Pair2)) { return true; }

            return false;
        }

        //public void ParticipateCount()
        //{
        //    this.Pair1.ParticipateCount();
        //    this.Pair2.ParticipateCount();
        //}

        public override string ToString()
        {
            return this.Pair1.ToString() + ", " + this.Pair2.ToString();
        }
    }
}
