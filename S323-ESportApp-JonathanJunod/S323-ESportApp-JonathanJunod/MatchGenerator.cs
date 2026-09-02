using DataSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S323_ESportApp_JonathanJunod
{
    public static class MatchGenerator
    {
        /// <summary>
        /// Generate fake data for a CS2 match
        /// </summary>
        /// <param name="player"></param>
        /// <param name="count"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static DataSeries<Cs2Match> GenerateCs2(string player, int count, int seed = 42)
        {
            var rng = new Random(seed);
            var maps = new[] { "Dust2", "Mirage", "Inferno", "Nuke", "Ancient" };
            var sides = new[] { "CT", "T" };
            var start = DateTime.Now;

            return DataSeries<Cs2Match>.From(
                Enumerable.Range(1, count)
                .Select(i => new DataPoint<Cs2Match>(
                    start.AddDays(i),
                    new Cs2Match(
                        player,                         // Player
                        maps[rng.Next(maps.Length)],    // Map
                        sides[rng.Next(sides.Length)],  // Side
                        kills: rng.Next(10, 27),        // Kills
                        deaths: rng.Next(6, 17),        // Deaths
                        assists: rng.Next(7),           // Assists
                        mvps: rng.Next(4),              // Mvps
                        won: rng.Next(2) == 1           // Won (0 = lost, 1 = won)
                    )
                ))
            );
        }
    }
}
