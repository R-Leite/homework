using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Competition
    {
        // 出場回数、各プレイヤーとのコンビ回数、同コード回数の格納変数を用意する
        public int part;
        public List<int> player_combi;
        public List<int> player_coat;

        public Competition()
        {
            part = 0;
            player_combi = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
            player_coat = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };

            // 一番少ない順に出力していく
        }
    }
}
