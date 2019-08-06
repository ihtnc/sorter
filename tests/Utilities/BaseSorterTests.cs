using Sorter.Services;
using Xunit;
using FluentAssertions;

namespace Sorter.Tests.Utilities
{
    public abstract class BaseSorterTests
    {
        protected abstract ISorter Initialise();

        [Theory]
        [InlineData(new []{0}, new []{0})]
        [InlineData(new []{1, 2, 3}, new []{1, 2, 3})]
        [InlineData(new []{3, 2, 1}, new []{1, 2, 3})]
        [InlineData(new []{4, 4, 4}, new []{4, 4, 4})]
        [InlineData(new []{3, -1, 4, -8}, new []{-8, -1, 3, 4})]
        [InlineData(new []{2, 3, 1, 3}, new []{1, 2, 3, 3})]
        [InlineData(new []{-5, -3, -4, -1}, new []{-5, -4, -3, -1})]
        [InlineData(new int[0], new int[0])]
        public virtual void Sort_Should_Handle_Integers(int[] items, int[] expected)
        {
            var sorter = Initialise();
            sorter.Sort(items).Should().BeEquivalentTo(expected, opt => opt.WithStrictOrdering());
        }

        [Theory]
        [InlineData(new []{"A"}, new []{"A"})]
        [InlineData(new []{"a", "b", "c"}, new []{"a", "b", "c"})]
        [InlineData(new []{"c", "b", "a"}, new []{"a", "b", "c"})]
        [InlineData(new []{"d", "d", "d"}, new []{"d", "d", "d"})]
        [InlineData(new []{"c", "b", "d", "a"}, new []{"a", "b", "c", "d"})]
        [InlineData(new []{"b", "c", "a", "c"}, new []{"a", "b", "c", "c"})]
        [InlineData(new []{"a", "c", "b", "d"}, new []{"a", "b", "c", "d"})]
        [InlineData(new string[0], new string[0])]
        public virtual void Sort_Should_Handle_Strings(string[] items, string[] expected)
        {
            var sorter = Initialise();
            sorter.Sort(items).Should().BeEquivalentTo(expected, opt => opt.WithStrictOrdering());
        }

        [Theory]
        [InlineData(new []{1, 2, 3}, new []{3, 2, 1})]
        [InlineData(new []{0}, new []{0})]
        [InlineData(new []{3, 2, 1}, new []{3, 2, 1})]
        [InlineData(new []{4, 4, 4}, new []{4, 4, 4})]
        [InlineData(new []{3, -1, 4, -8}, new []{4, 3, -1, -8})]
        [InlineData(new []{2, 3, 1, 3}, new []{3, 3, 2, 1})]
        [InlineData(new []{-5, -3, -4, -1}, new []{-1, -3, -4, -5})]
        [InlineData(new int[0], new int[0])]
        public virtual void Sort_With_Comparer_Should_Return_Correctly(int[] items, int[] expected)
        {
            var comparer = new InvertedComparer<int>();
            var sorter = Initialise();
            sorter.Sort(items, comparer).Should().BeEquivalentTo(expected, opt => opt.WithStrictOrdering());
        }
    }
}