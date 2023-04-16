using Project.Domain.Users;

namespace Project.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);
