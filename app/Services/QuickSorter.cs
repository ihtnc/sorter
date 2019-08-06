using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorter.Services
{
    public class QuickSorter : Sorter, ISorter
    {
        public override T[] Sort<T>(T[] items, IComparer<T> comparer)
        {
            try
            {
                StartTimer();

                var startIndex = 0;
                var endIndex = items.Count() - 1;

                Reorder(items, startIndex, endIndex, comparer);
                return items;
            }
            finally
            {
                StopTimer();
            }
        }

        private void Reorder<T>(T[] list, int startIndex, int endIndex, IComparer<T> comparer)
        {
            var tasks = new Stack<Tuple<int, int>>();
            tasks.Push(new Tuple<int, int>(startIndex, endIndex));

            while(tasks.Any())
            {
                var current = tasks.Pop();
                var start = current.Item1;
                var end = current.Item2;

                if(start >= end) { continue; }

                // reorder items such that
                //   all items left of the mid point is less than the value of the mid point
                //   all items right of the mid point is greater than the value of the mid point
                var midPointIndex = SwapAndGetMidPoint(list, start, end, comparer);

                // divide the list from the mid point and reorder each separately
                tasks.Push(new Tuple<int, int>(start, midPointIndex));
                tasks.Push(new Tuple<int, int>(midPointIndex + 1, end));
            }
        }

        private int SwapAndGetMidPoint<T>(T[] list, int start, int end, IComparer<T> comparer)
        {
            var mid = Math.DivRem(end - start, 2, out var rem);
            var midIndex = start + mid;
            var midValue = list[midIndex];

            while(start < end)
            {
                // find an item that is larger than the value of the mid point
                //   starting from the left most side of the list to the mid point
                var largerIndex = FindIndex(list, start,
                    (current) => current < midIndex,
                    (item) => comparer.Compare(item, midValue) >= 0,
                    (current) => current + 1);

                // find an item that is smaller than the value of the mid point
                //   starting from the right most side of the list to the mid point
                var smallerIndex = FindIndex(list, end,
                    (current) => current > midIndex,
                    (item) => comparer.Compare(item, midValue) <= 0,
                    (current) => current - 1);

                // if a larger or smaller item is found, swap the items' positions
                if(largerIndex >= 0 || smallerIndex >= 0)
                {
                    // if no larger item is found,
                    //   this means the mid point is the largest item on the left side
                    //   use the mid point if this is the case
                    start = largerIndex < 0 ? midIndex : largerIndex;

                    // if no smaller item is found,
                    //   this means the mid point is the smallest item on the right side
                    //   so use the mid point if this is the case
                    end = smallerIndex < 0 ? midIndex : smallerIndex;

                    var temp = list[start];
                    list[start] = list[end];
                    list[end] = temp;
                }

                start++;
                end--;
            }

            // return the index of the last smaller item found to be the next mid point
            return end;
        }

        private int FindIndex<T>(T[] list, int startIndex, Func<int, bool> loopCondition, Func<T, bool> findCondition, Func<int, int> nextIndex)
        {
            var found = false;
            while(loopCondition(startIndex) && !found)
            {
                if(findCondition(list[startIndex])) { found = true; }
                else { startIndex = nextIndex(startIndex); }
            }

            return found ? startIndex : -1;
        }
    }
}