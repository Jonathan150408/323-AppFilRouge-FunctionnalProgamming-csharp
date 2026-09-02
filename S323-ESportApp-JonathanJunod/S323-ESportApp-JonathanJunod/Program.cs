using DataSeries;
using static S323_ESportApp_JonathanJunod.MatchParser;

namespace S323_ESportApp_JonathanJunod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // --------------------------
            // Generate the history for Raphael
            // --------------------------
            var raphaelGenerated = MatchGenerator.GenerateCs2("Raphaël", 20);
            Console.WriteLine(raphaelGenerated.DataPoints.Count()); // 20

            //Func<Cs2Match, bool> isValid = m =>
            //    m.Kills + m.Assists <= 50 &&
            //    m.Deaths >= 1;

            //var raphaelValid = raphaelGenerated.Filter(isValid);
            //Console.WriteLine($"Avant : {raphaelGenerated.Count}, après : {raphaelValid.Count}");

            // --------------------------
            // Import data
            // --------------------------
            var valorant = DataSeries<ValorantMatch>.FromCsv("Data/valorant.csv", ParseValorant);
            var cs2 = DataSeries<Cs2Match>.FromCsv("Data/cs2.csv", ParseCs2);
            var lol = DataSeries<LolMatch>.FromCsv("Data/lol.csv", ParseLol);

            // --------------------------
            // Handle flags
            // --------------------------
            if (args.Length == 0 || args.Contains("--help"))
            {
                Console.WriteLine("Usage: EsportApp [--game valorant|cs2|lol]");
                return;
            }

            string? game = null;
            if (args.Contains("--game"))
                game = args[Array.IndexOf(args, "--game") + 1].ToLower();

            if (game == null || game == "valorant")
                Console.WriteLine($"Valorant : {valorant.DataPoints.Count()} matchs");
            if (game == null || game == "cs2")
                Console.WriteLine($"CS2      : {cs2.DataPoints.Count()} matchs");
            if (game == null || game == "lol")
                Console.WriteLine($"LoL      : {lol.DataPoints.Count()} matchs");
        }
    }
}
