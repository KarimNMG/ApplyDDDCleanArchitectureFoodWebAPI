using Project.Domain.Entities;

namespace Project.Application.Authentication.Common;

public record AuthenticationResult(
    User user,
    string Token);
