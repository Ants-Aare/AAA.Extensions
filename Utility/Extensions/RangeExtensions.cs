using System;

namespace AAA.Utility.Extensions
{
    public ref struct RangeEnumerator
    {
        int _current;
        readonly int _end;
        readonly int _next;

        public RangeEnumerator(Range range)
        {
            if (range.End.IsFromEnd)
            {
                throw new NotSupportedException();
            }

            if (range.Start.IsFromEnd)
            {
                throw new NotSupportedException();
            }

            _next = range.Start.Value <= range.End.Value ? 1 : -1;
            _current = range.Start.Value - _next;
            _end = range.End.Value;
        }

        public int Current => _current;

        public bool MoveNext()
        {
            _current += _next;
            return _current != _end;
        }
    }

    public static class RangeExtensions
    {
        public static RangeEnumerator GetAscendingEnumerator(this Range range)
            => new RangeEnumerator(range);

        /// <summary>
        /// Allows Range to be used as an IEnumerator so it can be used with linq / foreach
        /// </summary>
        public static RangeEnumerator GetEnumerator(this Range range)
            => new RangeEnumerator(range);
    }
}
