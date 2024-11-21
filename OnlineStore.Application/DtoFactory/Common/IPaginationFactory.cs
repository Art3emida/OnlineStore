namespace OnlineStore.Application.DtoFactory.Common;

using OnlineStore.Application.Query.Common;

public interface IPaginationFactory
{
    public IPagination Create(
        int total,
        int pageCount,
        int currentPage,
        int perPage
    );
}
