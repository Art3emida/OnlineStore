namespace OnlineStore.Domain.Model.Users;

using Microsoft.AspNetCore.Identity;
using OnlineStore.Domain.Api.Users;

public class User : IdentityUser<int>, IUser
{
    public string Name { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? StreetAddress { get; set; }
}
