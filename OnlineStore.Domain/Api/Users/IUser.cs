namespace OnlineStore.Domain.Api.Users;

public interface IUser
{
    int Id { get; set; }
    string UserName { get; set; }
    string Email { get; set; }
    string Name { get; set; }
    string? City { get; set; }
    string? State { get; set; }
    string? PostalCode { get; set; }
    string? StreetAddress { get; set; }
}
