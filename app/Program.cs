using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arg = new Arguments(args);
                if(arg.RunPerformanceTest)
                {
                    PerformanceTest.Run();
                    return;
                }

                var factory = new Factory();
                var sorter = factory.GetSorter(arg.Algorithm);

                var seed = Guid.NewGuid().GetHashCode();
                var random = new Random(seed);
                var count = arg.ItemCount ?? random.Next(arg.ItemMinCount, arg.ItemMaxCount);

                var items = arg.Items?.Any() == true ? arg.Items : GetRandomValues(count, arg.ItemMinValue, arg.ItemMaxValue);

                var original = GetString(arg.Verbose, items);
                Print(arg.Verbose, $"Items to sort: {original}");
                Print(arg.Verbose, $"Item count: {items.Length}");

                Print(arg.Verbose, $"Using algorithm: {arg.Algorithm}");
                var sorted = sorter.Sort(items);
                Print(arg.Verbose, $"Sorting done: {sorter.LastSortDuration}ms");

                var result = GetString(arg.Verbose, sorted);
                Print(arg.Verbose, $"Result: {result}");
                Output(arg.Verbose, result);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static int[] GetRandomValues(int itemCount, int lowerBoundValue, int upperBoundValue)
        {
            var seed = Guid.NewGuid().GetHashCode();
            var random = new Random(seed);
            var list = new List<int>();

            for(var i = 0; i < itemCount; i++)
            {
                list.Add(random.Next(lowerBoundValue, upperBoundValue));
            }

            return list.ToArray();
        }

        static void Print(bool verbose, string message)
        {
            if(!verbose) { return; }
            Console.WriteLine(message);
        }

        static void Output(bool verbose, string message)
        {
            if(verbose) { return; }
            Console.WriteLine(message);
        }

        static string GetString<T>(bool verbose, IEnumerable<T> list)
        {
            var separator = verbose ? ',' : ' ';
            var text = string.Join(separator, list);
            return verbose ? $"[{text}]" : text;
        }
    }
}
