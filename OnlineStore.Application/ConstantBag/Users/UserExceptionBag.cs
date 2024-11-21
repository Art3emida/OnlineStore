namespace OnlineStore.Application.ConstantBag.Users;

public static class UserExceptionBag
{
    public const string UserNotFound = "User with ID {0} not found.";
    public const string InvalidLoginAttempt = "Invalid login attempt.";
    public const string InvalidLoginAttemptUserLocked = "Your account has been locked due to too many failed login attempts.";
}
