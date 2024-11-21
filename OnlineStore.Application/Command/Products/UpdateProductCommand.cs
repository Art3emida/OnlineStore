namespace OnlineStore.Application.Command.Products;

using OnlineStore.Application.Dto.Common;

public record UpdateProductCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    List<string>? Photos
): IDto;
