using System;
using Sorter.Services;

namespace Sorter
{
    public class Factory : IFactory
    {
        public ISorter GetSorter(SorterTypeEnum type)
        {
            switch(type)
            {
                case SorterTypeEnum.InsertionSort:
                    return new InsertionSorter();
                case SorterTypeEnum.QuickSort:
                    return new QuickSorter();
                default:
                    throw new NotImplementedException($"Unsupported SorterType: {type}.");
            }
        }
    }
}