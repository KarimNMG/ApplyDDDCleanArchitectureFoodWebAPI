using Mapster;
using Project.Application.Users.Commands.UpdateUser;
using Project.Contracts.Users.UpdateUser;

namespace Project.Application.Mapping.Users;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateUserRequest Request, Guid UserId), UpdateUserCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }
}
