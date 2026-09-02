using DataSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S323_ESportApp_JonathanJunod
{
    public class MatchParser
    {

        /// <summary>
        /// Parse the data (cols) into an object ValorantMatch
        /// </summary>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static ValorantMatch ParseValorant(string[] cols) => new ValorantMatch(
            cols[1],
            cols[2],
            int.Parse(cols[3]),
            int.Parse(cols[4]),
            int.Parse(cols[5]),
            int.Parse(cols[6]),
            int.Parse(cols[7]),
            cols[8] == null
        );

        /// <summary>
        /// Parse the data (cols) into an object LolMatch
        /// </summary>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static LolMatch ParseLol(string[] cols) => new LolMatch(
            cols[1],
            cols[2],
            int.Parse(cols[3]),
            int.Parse(cols[4]),
            int.Parse(cols[5]),
            int.Parse(cols[6]),
            int.Parse(cols[7]),
            cols[8] == null
        );

        /// <summary>
        /// Parse the data (cols) into an object Cs2Match
        /// </summary>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static Cs2Match ParseCs2(string[] cols) => new Cs2Match(
            cols[1],
            cols[2],
            cols[3],
            int.Parse(cols[4]),
            int.Parse(cols[5]),
            int.Parse(cols[6]),
            int.Parse(cols[7]),
            cols[8] == null
        );
    }
}
