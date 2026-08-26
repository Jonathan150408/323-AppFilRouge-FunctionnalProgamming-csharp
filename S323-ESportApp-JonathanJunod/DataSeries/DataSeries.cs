using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeries
{
    public class DataSeries<T>
    {
        private readonly IEnumerable<T> _data;
        public int Count => _data.Count();
        public IEnumerable<T> Values => _data;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="data"></param>
        private DataSeries(IEnumerable<T> data) => _data = data;

        /// <summary>
        /// Constructor "From"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataSeries<T> From(IEnumerable<T> source)
            => new DataSeries<T>(source);

        /// <summary>
        /// Constructor "FromCSV"
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        public static DataSeries<T> FromCsv(string path, Func<string[], T> parser)
        {
            var lines = File.ReadAllLines(path).Skip(1);
            return new DataSeries<T>(lines.Select(line => parser(line.Split(','))));
        }
    }
}
