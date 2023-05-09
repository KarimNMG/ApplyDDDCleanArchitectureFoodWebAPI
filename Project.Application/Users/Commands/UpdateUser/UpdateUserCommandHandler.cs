using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.UserAggregate;

namespace Project.Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, ErrorOr<string>>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = returnUser(request.UserId) as User;
        if (user is null)
            return Domain.Common.Errors.DomainErrors.User.UserNotFound;


        user.SetUserFirstName(request.FirstName);
        user.SetUserLastName(request.LastName);

        var ret = _userRepository.UpdateAsync(user);
        
        return ret.ToString();
    }

    private User? returnUser(Guid userId) => _userRepository.GetUserById(userId);
}