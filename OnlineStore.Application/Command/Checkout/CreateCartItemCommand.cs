namespace OnlineStore.Application.Command.Checkout;

using OnlineStore.Application.Dto.Common;

public record CreateCartItemCommand(
    string CartId,
    int ProductId,
    string Name,
    int Quantity,
    decimal Price
): IDto;
