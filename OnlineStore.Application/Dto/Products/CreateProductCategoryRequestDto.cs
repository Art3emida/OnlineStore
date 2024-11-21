namespace OnlineStore.Application.Dto.Products;

using OnlineStore.Application.Dto.Common;

public record CreateProductCategoryRequestDto(
    string Name,
    string Description,
    string? Photo
) : IDto;
