namespace OnlineStore.Application.Dto.Products;

using OnlineStore.Application.Dto.Common;

public record CreateProductRequestDto(
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    List<string>? Photos
) : IDto;
