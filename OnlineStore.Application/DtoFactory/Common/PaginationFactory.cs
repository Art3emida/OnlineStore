namespace OnlineStore.Application.DtoFactory.Common;

using OnlineStore.Application.Query.Common;

public class PaginationFactory : IPaginationFactory
{
    public IPagination Create(
        int total,
        int pageCount,
        int currentPage,
        int perPage
    ) {
        return new Pagination(
            Total: total,
            PageCount: pageCount,
            CurrentPage: currentPage,
            PerPage: perPage
        );
    }
}
