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
        public static IEnumerable<Cs2Match> GenerateCs2(string player, int count, int seed = 42)
        {
            var rng = new Random(seed);
            var maps = new[] { "Dust2", "Mirage", "Inferno", "Nuke", "Ancient" };
            var sides = new[] { "CT", "T" };

            return Enumerable.Range(1, count)
                .Select(i => new Cs2Match(
                    player,                         // Player
                    maps[rng.Next(maps.Length)],    // Map
                    sides[rng.Next(sides.Length)],  // Side
                    kills: rng.Next(10, 27),        // Kills
                    deaths: rng.Next(6, 17),        // Deaths
                    assists: rng.Next(7),           // Assists
                    mvps: rng.Next(4),              // Mvps
                    won: rng.Next(2) == 1           // Won (0 = lost, 1 = won)
                    
                ));
        }
    }
}
