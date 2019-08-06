using System;
using Sorter.Services;
using Xunit;
using FluentAssertions;

namespace Sorter.Tests
{
    public class FactoryTests
    {
        private readonly IFactory _factory;

        public FactoryTests()
        {
            _factory = new Factory();
        }

        [Theory]
        [InlineData(SorterTypeEnum.InsertionSort, typeof(InsertionSorter))]
        [InlineData(SorterTypeEnum.QuickSort, typeof(QuickSorter))]
        public void GetSorter_Should_Return_Correct_Type(SorterTypeEnum type, Type sorterType)
        {
            var sorter = _factory.GetSorter(type);
            sorter.Should().BeOfType(sorterType);
        }
    }
}
