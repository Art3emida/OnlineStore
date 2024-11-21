namespace OnlineStore.Application.Query.Products;

[Flags]
public enum ProductSort
{
    None = 0,
    NameAsc = 1,
    NameDesc = 2,
    PriceAsc = 4,
    PriceDesc = 8,
    CreatedAtAsc = 16,
    CreatedAtDesc = 32,
    UpdatedAtAsc = 64,
    UpdatedAtDesc = 128
}
