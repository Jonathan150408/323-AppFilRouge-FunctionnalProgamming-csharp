using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeries
{
    public class ValorantMatch
    {
        public string Player { get; }
        public string Agent { get; }
        public int Kills { get; }
        public int Deaths { get; }
        public int Assists { get; }
        public int Headshots { get; }
        public int RoundsWon { get; }
        public bool Won { get; }

        public ValorantMatch(string player, string agent, int kills, int deaths, int assists, int headshots, int roundswon, bool won)
        {
            Player = player; Agent = agent; Kills = kills; Deaths = deaths; Assists = assists; Headshots = headshots; RoundsWon = roundswon; Won = won;
        }
    }
}
