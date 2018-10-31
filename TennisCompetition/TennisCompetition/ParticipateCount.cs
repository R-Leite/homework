using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class ParticipateCount
    {
        public Dictionary<Player, int> Player;
        public Dictionary<Pair, int> Pair;
        public Dictionary<Match, int> Match;

        public ParticipateCount(List<Player> players, List<Pair> pairs, List<Match> matches)
        {
            players.ForEach(p => this.Player.Add(p, 0));
            pairs.ForEach(p => this.Pair.Add(p, 0));
            matches.ForEach(m => this.Match.Add(m, 0));
        }
    }
}
