using System;
using System.Collections.Generic;

namespace Sorter
{
    public class Arguments
    {
        public const SorterTypeEnum DEFAULT_ALGORITHM = SorterTypeEnum.InsertionSort;
        public const int DEFAULT_ITEM_MIN_COUNT = 10;
        public const int DEFAULT_ITEM_MAX_COUNT = 100;
        public const int DEFAULT_LOWER_BOUND = -1000;
        public const int DEFAULT_UPPER_BOUND = 1000;

        public Arguments(string[] args)
        {
            Algorithm = DEFAULT_ALGORITHM;
            ItemMinCount = DEFAULT_ITEM_MIN_COUNT;
            ItemMaxCount = DEFAULT_ITEM_MAX_COUNT;
            ItemMinValue = DEFAULT_LOWER_BOUND;
            ItemMaxValue = DEFAULT_UPPER_BOUND;
            Verbose = false;
            RunPerformanceTest = false;

            for(var i = 0; i < args.Length; i++)
            {
                var name = args[i];

                if(string.Equals(name, "--compare", StringComparison.OrdinalIgnoreCase))
                {
                    RunPerformanceTest = true;
                }
                else if(string.Equals(name, "--algorithm", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-a", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    Algorithm = GetAlgorithm(args[i]);
                }
                else if(string.Equals(name, "--count", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-c", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    ItemCount = GetInt("count", args[i]);
                }
                else if(string.Equals(name, "--mincount", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-nc", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    ItemMinCount = GetInt("mincount", args[i]);
                }
                else if(string.Equals(name, "--maxcount", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-xc", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    ItemMaxCount = GetInt("maxcount", args[i]);
                }
                else if(string.Equals(name, "--minvalue", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-nv", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    ItemMinValue = GetInt("minvalue", args[i]);
                }
                else if(string.Equals(name, "--maxvalue", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-xv", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    ItemMaxValue = GetInt("maxvalue", args[i]);
                }
                else if(string.Equals(name, "--verbose", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-v", StringComparison.OrdinalIgnoreCase))
                {
                    Verbose = true;
                }
                else if(string.Equals(name, "--items", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(name, "-i", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    Items = GetItems(args, i);
                }

                ValidateValues("mincount", ItemMinCount, "maxcount", ItemMaxCount);
                ValidateValues("minvalue", ItemMinValue, "maxvalue", ItemMaxValue);
            }
        }

        public SorterTypeEnum Algorithm { get; }
        public int[] Items { get; }
        public int? ItemCount { get; }
        public int ItemMinCount { get; }
        public int ItemMaxCount { get; }
        public int ItemMinValue { get; }
        public int ItemMaxValue { get; }
        public bool Verbose { get; }
        public bool RunPerformanceTest { get; }

        private static SorterTypeEnum GetAlgorithm(string value)
        {
            if(Enum.TryParse<SorterTypeEnum>(value, true, out var type))
            {
                return type;
            }

            throw new ArgumentException($"Invalid algorithm value: {value}.");
        }

        private static int GetInt(string argumentName, string value)
        {
            if(int.TryParse(value, out var intValue))
            {
                return intValue;
            }

            throw new ArgumentException($"Invalid {argumentName} value: {value}.");
        }

        private static int[] GetItems(string[] args, int startIndex)
        {
            var list = new List<int>();

            for(var i = startIndex; i < args.Length; i++)
            {
                if(int.TryParse(args[i], out var value))
                {
                    list.Add(value);
                }
                else
                {
                    throw new ArgumentException($"items contain an invalid value: {args[i]}.");
                }
            }

            return list.ToArray();
        }

        private static void ValidateValues(string minArgumentName, int minValue, string maxArgumentName, int maxValue)
        {
            if(minValue > maxValue) { throw new ArgumentException($"{minArgumentName} should not be greater than {maxArgumentName}."); }
        }
    }
}