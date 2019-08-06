using Sorter.Services;
using Sorter.Tests.Utilities;
using Xunit;
using FluentAssertions;

namespace Sorter.Tests.Services
{
    public class QuickSorterTests : BaseSorterTests
    {
        protected override ISorter Initialise() => new QuickSorter();

        [Fact]
        public void Should_Fix_Sorting_Bug()
        {
            // TODO: Not yet working: QuickSort returns [0, 4, 1, 2, 3, 5, 7, 8] for this set
            var sorter = Initialise();
            var unsorted = new [] {1, 8, 0, 5, 3, 2, 7, 4};
            var sorted = new [] {0, 1, 2, 3, 4, 5, 7, 8};
            sorter.Sort(unsorted).Should().BeEquivalentTo(sorted, opt => opt.WithStrictOrdering());
        }
    }
}