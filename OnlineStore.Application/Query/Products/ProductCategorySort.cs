namespace OnlineStore.Application.Query.Products;

[Flags]
public enum ProductCategorySort
{
    NameAsc = 1,
    NameDesc = 2,
    CreatedAtAsc = 16,
    CreatedAtDesc = 32,
    UpdatedAtAsc = 64,
    UpdatedAtDesc = 128
}
