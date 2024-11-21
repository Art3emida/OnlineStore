namespace OnlineStore.Application.Query.Products;

[Flags]
public enum ProductInclude
{
    None = 0,
    Photos = 1,
    Category = 2
}
