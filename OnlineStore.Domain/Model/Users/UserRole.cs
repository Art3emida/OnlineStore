namespace OnlineStore.Domain.Model.Users;

using Microsoft.AspNetCore.Identity;
using OnlineStore.Domain.Api.Users;

public class UserRole : IdentityRole<int>, IUserRole
{
}
