namespace OnlineStore.Application.Query.Common;

public interface IPagination
{
    int Total { get; }
    int PageCount { get; }
    int CurrentPage { get; }
    int PerPage { get; }
    bool HasNextPage { get; }
}
