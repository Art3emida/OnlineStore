namespace OnlineStore.Application.Query.Common;

public interface IPaginationControl
{
    int? Limit { get; }
    int? Offset { get; }
}
