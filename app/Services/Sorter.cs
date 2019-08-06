using System.Collections.Generic;
using System.Diagnostics;

namespace Sorter.Services
{
    public abstract class Sorter : ISorter
    {
        private readonly Stopwatch _watch;

        public Sorter()
        {
            _watch = new Stopwatch();
        }

        public void StartTimer()
        {
            _watch.Reset();
            _watch.Start();
        }
        public void StopTimer() { _watch.Stop(); }

        public long LastSortDuration => _watch.ElapsedMilliseconds;

        public T[] Sort<T>(T[] items)
        {
            return Sort(items, Comparer<T>.Default);
        }

        public abstract T[] Sort<T>(T[] items, IComparer<T> comparer);
    }
}