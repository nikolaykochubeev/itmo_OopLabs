using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        private readonly List<Pair> _pairs;
        public Schedule(List<Pair> pairs = null)
        {
            _pairs = pairs ?? new List<Pair>();
        }

        public IReadOnlyList<Pair> Pairs => _pairs;

        public void AddPair(Pair pair)
        {
            if (_pairs.FirstOrDefault(pair1 => pair1.DayOfWeek == pair.DayOfWeek && pair1.Time == pair.Time) is not null)
                throw new IsuExtraException("Something was wrong");
            _pairs.Add(pair);
        }

        public void RemovePair(Pair pair)
        {
            if (!_pairs.Contains(pair))
                throw new IsuExtraException("Pair doesn't exist");
            _pairs.Remove(pair);
        }
    }
}