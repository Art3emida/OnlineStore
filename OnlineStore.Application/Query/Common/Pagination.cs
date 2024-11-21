namespace OnlineStore.Application.Query.Common;

public record Pagination(
    int Total,
    int PageCount,
    int CurrentPage,
    int PerPage
) : IPagination {
    public bool HasNextPage => CurrentPage < PageCount;
}
