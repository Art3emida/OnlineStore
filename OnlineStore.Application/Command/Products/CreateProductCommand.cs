namespace OnlineStore.Application.Command.Products;

using OnlineStore.Application.Dto.Common;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    List<string>? Photos
): IDto;
