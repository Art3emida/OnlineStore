namespace OnlineStore.Application.DtoFactory.Common;

using OnlineStore.Application.Query.Common;

public interface IListStructureFactory<T>
{
    IListStructure<T> Create(
        IEnumerable<T> data,
        int total,
        int pageCount,
        int currentPage,
        int perPage
    );
}
