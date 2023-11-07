using MediatR;
using Project.Application.Authentication.Common;
using Project.Domain.Common.Errors;

namespace Project.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string firstName,
    string lastName,
    string email,
    string password) : IRequest<Result<AuthenticationResult>>;
