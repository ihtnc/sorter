using System.Collections.Generic;

namespace Sorter.Services
{
    public interface ISorter
    {
        long LastSortDuration { get; }
        T[] Sort<T>(T[] items);
        T[] Sort<T>(T[] items, IComparer<T> comparer);
    }
}