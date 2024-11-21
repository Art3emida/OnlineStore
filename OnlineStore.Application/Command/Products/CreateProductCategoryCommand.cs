namespace OnlineStore.Application.Command.Products;

using OnlineStore.Application.Dto.Common;

public record CreateProductCategoryCommand(
    string Name,
    string Description,
    string? Photo
): IDto;
