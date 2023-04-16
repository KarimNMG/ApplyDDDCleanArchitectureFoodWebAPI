using Project.Domain.Entities;

namespace Project.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);
