using DataSeries;
using static S323_ESportApp_JonathanJunod.MatchParser;

namespace S323_ESportApp_JonathanJunod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // --------------------------
            // Program's variables
            // --------------------------
            List<string> PlayersToGenerate = [];
            List<DataSeries<Cs2Match>> CS2Matches = [];
            List<DataSeries<LolMatch>> LolMatches = [];
            List<DataSeries<ValorantMatch>> valorantMatches = [];

            // --------------------------
            // Import data
            // --------------------------
            var valorant = DataSeries<ValorantMatch>.FromCsv("Data/valorant.csv", ParseValorant);
            var cs2 = DataSeries<Cs2Match>.FromCsv("Data/cs2.csv", ParseCs2);
            var lol = DataSeries<LolMatch>.FromCsv("Data/lol.csv", ParseLol);

            // --------------------------
            // Filter
            // --------------------------
            Func<ValorantMatch, bool> isWin = m => m.Won;
            Func<ValorantMatch, bool> isHighScore = m => m.Kills > 20;

            // Combinaison : un nouveau prédicat (victoire éclatante) construit à partir des deux autres
            Func<ValorantMatch, bool> isCrushingWin = m => isWin(m) && isHighScore(m);
            var top = valorant.Filter(isCrushingWin);
            Console.WriteLine($"Was the loaded valorant match a crushing win ? : {top != null}");

            // --------------------------
            // Remove the liards / cheaters
            // --------------------------
            // Valorant : kills plausibles pour un match compétitif
            var valorantValid = valorant.RemoveOutliers(m =>
                m.Kills >= 0 && m.Kills <= 50 &&
                m.Deaths >= 1 && m.Deaths <= 30 &&
                m.Assists >= 0
            );

            // CS2 : contraintes similaires
            var cs2Valid = cs2.RemoveOutliers(m =>
                m.Kills + m.Assists <= 50 &&
                m.Deaths >= 1
            );

            // LoL : le support a structurellement peu de kills
            var lolValid = lol.RemoveOutliers(m =>
                m.Kills <= 10 &&
                m.Deaths >= 1 &&
                m.Assists >= 0 &&
                m.Cs >= 0
            );

            // --------------------------
            // Filter
            // --------------------------
            // Léa a-t-elle au moins un match avec plus de 20 kills ?
            Console.WriteLine($"Léa a-t-elle au moins un match avec plus de 20 kills ? : {valorantValid.HasAny(m => m.Kills > 20)}");

            // Tous les matchs de Noé ont-ils au moins 1 mort ?
            Console.WriteLine($"Tous les matchs de Noé ont-ils au moins 1 mort ? : {lolValid.AllMatch(m => m.Deaths >= 1)}");

            // --------------------------
            // Handle flags
            // --------------------------
            bool hasFlag = true;
            string? game = null;
            string? generationRequest = null;

            if (args.Length == 0 || args.Contains("--help"))
            {
                Console.WriteLine("Usage: EsportApp [--game valorant|cs2|lol] [--generate player|all]");
                hasFlag = false;
            }

            if (hasFlag)
            {
                // --game
                if (args.Contains("--game"))
                {
                    game = args[Array.IndexOf(args, "--game") + 1].ToLower();
                }
                if (game == "all" || game == "valorant")
                    Console.WriteLine($"Valorant : {valorantValid.DataPoints.Count()} matchs");
                if (game == "all" || game == "cs2")
                    Console.WriteLine($"CS2      : {cs2Valid.DataPoints.Count()} matchs");
                if (game == "all" || game == "lol")
                    Console.WriteLine($"LoL      : {lolValid.DataPoints.Count()} matchs");

                // --generate
                if (args.Contains("--generate"))
                {
                    generationRequest = args[Array.IndexOf(args, "--generate") + 1].ToLower();
                    // Fill in the list with all generation request
                    PlayersToGenerate = (generationRequest == "all" || generationRequest == null) ? ["Raphael", "Kiara", "Dylan", "Noé"] : [generationRequest];

                    PlayersToGenerate.ForEach(p =>
                    {
                        CS2Matches.Add(MatchGenerator.GenerateCs2(p, 10));
                        LolMatches.Add(MatchGenerator.GenerateLol(p, 10));
                        valorantMatches.Add(MatchGenerator.GenerateValorant(p, 10));
                    });
                }

            }

            // --------------------------
            // Export to CSV
            // --------------------------
            string currDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string workDir = Directory.GetCurrentDirectory();
            Directory.CreateDirectory($"{workDir}/Data/Exports");

            CS2Matches.ForEach(m =>
            {
                CSVExporter.ExportCs2(m, $"{workDir}/Data/Exports/{currDate}CS2.csv");
            });
            LolMatches.ForEach(m =>
            {
                CSVExporter.ExportLol(m, $"{workDir}/Data/Exports/{currDate}Lol.csv");
            });
            CS2Matches.ForEach(m =>
            {
                CSVExporter.ExportCs2(m, $"{workDir}/Data/Exports/{currDate}CS2.csv");
            });
        }
    }
}
