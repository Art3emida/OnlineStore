namespace OnlineStore.Application.Query.Common;

public interface IListStructure<T>
{
    IEnumerable<T> Items { get; }
    IPagination Pagination { get; }
}
