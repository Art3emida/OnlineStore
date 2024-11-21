namespace OnlineStore.Application.Dto.Products;

using OnlineStore.Application.Dto.Common;

public record UpdateProductCategoryRequestDto(
    int Id,
    string Name,
    string Description,
    string? Photo
) : IDto;
