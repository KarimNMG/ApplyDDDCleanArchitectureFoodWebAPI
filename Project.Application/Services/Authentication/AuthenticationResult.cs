using Project.Domain.Entities;

namespace Project.Application.Services.Authentication;

public record AuthenticationResult(
    User user ,
    string Token);
