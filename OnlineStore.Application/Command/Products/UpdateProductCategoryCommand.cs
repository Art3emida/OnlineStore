namespace OnlineStore.Application.Command.Products;

using OnlineStore.Application.Dto.Common;

public record UpdateProductCategoryCommand(
    int Id,
    string Name,
    string Description,
    string? Photo
): IDto;
