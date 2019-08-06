using System.Collections.Generic;

namespace Sorter.Tests.Utilities
{
    public class InvertedComparer<T> : IComparer<T>
    {
        private static IComparer<T> _comparer = Comparer<T>.Default;

        public int Compare(T x, T y)
        {
            return _comparer.Compare(x, y) * -1;
        }
    }
}