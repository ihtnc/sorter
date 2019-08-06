using Sorter.Services;

namespace Sorter
{
    public interface IFactory
    {
        ISorter GetSorter(SorterTypeEnum type);
    }
}