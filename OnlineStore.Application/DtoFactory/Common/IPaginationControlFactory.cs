namespace OnlineStore.Application.DtoFactory.Common;

using OnlineStore.Application.Query.Common;

public interface IPaginationControlFactory : IDtoFactory
{
    public IPaginationControl Create(
        int? limit = null,
        int? offset = null
    );
}
