namespace OnlineStore.Application.DtoFactory.Common;

using OnlineStore.Application.Query.Common;

public class PaginationControlFactory : IPaginationControlFactory
{
    public IPaginationControl Create(
        int? limit = null,
        int? offset = null
    ) {
        return new PaginationControl(
            Limit: limit,
            Offset: offset
        );
    }
}
