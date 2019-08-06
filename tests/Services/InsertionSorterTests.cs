using Sorter.Services;
using Sorter.Tests.Utilities;

namespace Sorter.Tests.Services
{
    public class InsertionSorterTests : BaseSorterTests
    {
        protected override ISorter Initialise() => new InsertionSorter();
    }
}