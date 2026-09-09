using DataSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S323_ESportApp_JonathanJunod
{
    public static class Normalizer
    {
        public static List<double> Normalize(DataSeries<double> kda)
        {
            double min = kda.Values.Min();
            double max = kda.Values.Max();

            return kda.Values
                .Select(v => (v - min) / (max - min))
                .ToList();
        }
    }
}
