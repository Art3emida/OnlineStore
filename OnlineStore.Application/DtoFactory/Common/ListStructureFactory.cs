namespace OnlineStore.Application.DtoFactory.Common;

using OnlineStore.Application.Query.Common;

public class ListStructureFactory<T> : IListStructureFactory<T>
{
    private readonly IPaginationFactory _paginationFactory;

    public ListStructureFactory(
        IPaginationFactory paginationFactory
    ) {
        _paginationFactory = paginationFactory;
    }
    
    public IListStructure<T> Create(
        IEnumerable<T> data,
        int total,
        int pageCount,
        int currentPage,
        int perPage
    ) {
        IPagination pagination = _paginationFactory.Create(
            total: total,
            pageCount: pageCount,
            currentPage: currentPage,
            perPage: perPage
        );

        return new ListStructure<T>(
            Items: data,
            Pagination: pagination
        );
    }
}
