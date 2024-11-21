namespace OnlineStore.Application.Query.Common;

public record ListStructure<T>(
    IEnumerable<T> Items,
    IPagination Pagination
) : IListStructure<T>;
