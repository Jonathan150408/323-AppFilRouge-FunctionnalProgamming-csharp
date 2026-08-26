using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeries
{
    public class LolMatch
    {
        public string Player { get; }
        public string Champion { get; }
        public string Role { get; set; }
        public int Kills { get; }
        public int Deaths { get; }
        public int Assists { get; }
        public int Cs { get; }
        public int VisionScore { get; }
        public bool Won { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        /// <param name="champion"></param>
        /// <param name="kills"></param>
        /// <param name="deaths"></param>
        /// <param name="assists"></param>
        /// <param name="cs"></param>
        /// <param name="visionScore"></param>
        /// <param name="won"></param>
        public LolMatch(string player, string champion, string role, int kills, int deaths, int assists, int cs, int visionScore, bool won)
        {
            Player = player;
            Champion = champion;
            Role = role;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            Cs = cs;
            VisionScore = visionScore;
            Won = won;
        }
    }
}
