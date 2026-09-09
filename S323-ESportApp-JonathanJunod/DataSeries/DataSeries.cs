using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace DataSeries
{
    public class DataSeries<T>
    {
        private readonly IEnumerable<DataPoint<T>> _data;
        public IEnumerable<T> Values => _data.Select(dp => dp.Value);
        public IEnumerable<DataPoint<T>> DataPoints => _data;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="data"></param>
        private DataSeries(IEnumerable<DataPoint<T>> data) => _data = data;

        /// <summary>
        /// Constructor "From"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataSeries<T> From(IEnumerable<DataPoint<T>> source) => new DataSeries<T>(source);

        /// <summary>
        /// Constructor "FromCSV"
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        public static DataSeries<T> FromCsv(string path, Func<string[], T> parser)
        {
            var lines = File.ReadAllLines(path).Skip(1);

            return new DataSeries<T>(lines.Select(line =>
            {
                var cols = line.Split(',');
                return new DataPoint<T>(DateTime.Parse(cols[0]), parser(cols));
            }));
        }

        /// <summary>
        /// Applies the filter and order datapoints by date
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public DataSeries<T> FilterByDate(Func<DateTime, bool> predicate)
            => new DataSeries<T>(_data.Where(dp => predicate(dp.Timestamp)));

        public DataSeries<T> Filter(Func<T, bool> predicate) 
            => new DataSeries<T>(_data.Where(dp => predicate(dp.Value)));

        public DataSeries<T> RemoveOutliers(Func<T, bool> isValid)
            => Filter(isValid);

        public bool HasAny(Func<T, bool> predicate)
            => _data.Any(dp => predicate(dp.Value));
        public bool AllMatch(Func<T, bool> predicate)
            => _data.All(dp => predicate(dp.Value));

        public DataSeries<TResult> Transform<TResult>(Func<T, TResult> mapper)
            => DataSeries<TResult>.From(_data.Select(dp => new DataPoint<TResult>( dp.Timestamp, mapper(dp.Value))));
    }
}
