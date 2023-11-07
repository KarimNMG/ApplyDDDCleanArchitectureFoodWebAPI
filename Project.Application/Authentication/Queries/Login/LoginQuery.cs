using MediatR;
using Project.Application.Authentication.Common;
using Project.Domain.Common.Errors;

namespace Project.Application.Authentication.Queries.Login;

public sealed record LoginQuery(
    string email,
    string password) : IRequest<Result<AuthenticationResult>>;