using System;
using System.Linq;

namespace Sorter
{
    public static class PerformanceTest
    {
        public static void Run()
        {
            Console.WriteLine("START: Performance tests");

            var random = GetRandomItems();
            RunTest("Random", random);

            var semi = GetRandomAscendingItems();
            RunTest("Random Ascending", semi);

            var descending = GetDescendingItems();
            RunTest("Descending", descending);

            var ascending = GetAscendingItems();
            RunTest("Ascending", ascending);

            var constants = GetConstantItems();
            RunTest("Constants", constants);

            Console.WriteLine("END: Performance tests");
        }

        private static int[] GetRandomItems()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var count = 50000;
            var items = new int[count];
            for(var i = 0; i < count; i++) { items[i] = random.Next(); }
            return items;
        }

        private static int[] GetRandomAscendingItems()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var count = 50000;
            var groupSize = 10000;
            var items = new int[count];
            for(var i = 0; i < count; i++)
            {
                var group = i % groupSize;
                var offset = 3500 * group;
                var start = 8000 + offset;
                var end = 15000 + offset;
                items[i] = random.Next(start, end);
            }
            return items;
        }

        private static int[] GetDescendingItems()
        {
            var count = 50000;
            var items = new int[count];
            for(var i = 0; i < count; i++) { items[i] = count - i; }
            return items;
        }

        private static int[] GetAscendingItems()
        {
            var count = 50000;
            var items = new int[count];
            for(var i = 0; i < count; i++) { items[i] = i; }
            return items;
        }

        private static int[] GetConstantItems()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var constant = random.Next();
            var count = 50000;
            var items = new int[count];
            for(var i = 0; i < count; i++) { items[i] = constant; }
            return items;
        }

        private static void RunTest<T>(string testName, T[] items)
        {
            var types = Enum.GetValues(typeof(SorterTypeEnum)).Cast<SorterTypeEnum>();
            var factory = new Factory();

            Console.WriteLine($"TEST: {testName.PadRight(16)} {items.Length} item(s)");

            foreach(var type in types)
            {
                var sorter = factory.GetSorter(type);
                Console.Write($"  {type.ToString().PadRight(20)}: ");
                sorter.Sort(items);
                Console.WriteLine($"{sorter.LastSortDuration.ToString().PadLeft(10)}ms");
            }
        }
    }
}