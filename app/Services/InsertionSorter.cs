using System.Collections.Generic;

namespace Sorter.Services
{
    public class InsertionSorter : Sorter, ISorter
    {
        public override T[] Sort<T>(T[] items, IComparer<T> comparer)
        {
            try
            {
                StartTimer();

                var lower = new Stack<T>();
                var upper = new Stack<T>();

                foreach(var item in items)
                {
                    EvaluateOrder(lower, upper, item, comparer);
                }

                return ToArray(lower, upper);
            }
            finally
            {
                StopTimer();
            }
        }

        private void EvaluateOrder<T>(Stack<T> lower, Stack<T> upper, T item, IComparer<T> comparer)
        {
            // traverse the upper group and transfer values less than the item to the lower group
            while(upper.Count > 0 && comparer.Compare(item, upper.Peek()) > 0)
            {
                lower.Push(upper.Pop());
            }

            // traverse the lower group and transfer values greater than the item to the upper group
            while(lower.Count > 0 && comparer.Compare(lower.Peek(), item) > 0)
            {
                upper.Push(lower.Pop());
            }

            // at this point all the items in the lower group are less than the item
            //   and all items in the upper group are greater than the item
            //   so inserting it on either group doesn't matter
            lower.Push(item);
        }

        private T[] ToArray<T>(Stack<T> lower, Stack<T> upper)
        {
            while(lower.Count > 0)
            {
                upper.Push(lower.Pop());
            }

            return upper.ToArray();
        }
    }
}