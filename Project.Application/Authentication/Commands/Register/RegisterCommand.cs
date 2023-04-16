using ErrorOr;
using MediatR;
using Project.Application.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string firstName,
    string lastName,
    string email,
    string password) : IRequest<ErrorOr<AuthenticationResult>>;
