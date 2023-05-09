using Mapster;
using Project.Application.Menus.Commands.CreateMenu;
using Project.Contracts.Menus;
using Project.Domain.MenuAggregate.Entities;
using Project.Domain.MenuAggregate;
using Project.Contracts.Users.UpdateUser;
using Project.Application.Users.Commands.UpdateUser;

namespace Project.WebApi.Common.Mapping.Users;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateUserRequest Request, Guid UserId), UpdateUserCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }
}
