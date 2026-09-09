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

        /// <summary>
        /// Generate fake data for a CS2 match
        /// </summary>
        /// <param name="player"></param>
        /// <param name="count"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static DataSeries<LolMatch> GenerateLol(string player, int count, int seed = 27)
        {
            var rng = new Random(seed);
            var champions = new[] { "Ahri", "Lee Sin", "Jinx", "Thresh", "Yasuo", "Lux" };
            var start = DateTime.Now;

            return DataSeries<LolMatch>.From(
                Enumerable.Range(1, count)
                .Select(i => new DataPoint<LolMatch>(
                    start.AddDays(i),
                    new LolMatch(
                        player,                                 // Player
                        champion: champions[rng.Next(6)],       // Champion
                        role: "Support",                    // Role <-- Fixe pour le moment
                        kills: rng.Next(10, 27),                // Kills
                        deaths: rng.Next(6, 17),                // Deaths
                        assists: rng.Next(7),                   // Assists
                        cs: rng.Next(100, 300),                 // CS
                        visionScore: rng.Next(10, 60),          // VisionScore
                        won: rng.Next(2) == 1                   // Won (0 = lost, 1 = won)
                    )
                ))
            );
        }

        /// <summary>
        /// Generate fake data for a CS2 match
        /// </summary>
        /// <param name="player"></param>
        /// <param name="count"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static DataSeries<ValorantMatch> GenerateValorant(string player, int count, int seed = 319)
        {
            var rng = new Random(seed);
            var agents = new[] { "Jett", "Reyna", "Sova", "Sage", "Omen", "Killjoy" };
            var start = DateTime.Now;

            return DataSeries<ValorantMatch>.From(
                Enumerable.Range(1, count)
                .Select(i => new DataPoint<ValorantMatch>(
                    start.AddDays(i),
                    new ValorantMatch(
                        player,                         // Player
                        agent: agents[rng.Next(6)],     // Agent
                        kills: rng.Next(10, 27),        // Kills
                        deaths: rng.Next(6, 17),        // Deaths
                        assists: rng.Next(7),           // Assists
                        headshots: rng.Next(5, 20),     // headshots
                        roundswon: rng.Next(1, 10),     // RoundsWon
                        won: rng.Next(2) == 1           // Won (0 = lost, 1 = won)
                    )
                ))
            );
        }
    }
}
