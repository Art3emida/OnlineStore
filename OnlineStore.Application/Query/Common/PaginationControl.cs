namespace OnlineStore.Application.Query.Common;

public record PaginationControl(
    int? Limit,
    int? Offset
) : IPaginationControl;
