using Mapster;
using Project.Application.Menus.Commands.CreateMenu;
using Project.Contracts.Menus;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Entities;

namespace Project.WebApi.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
            .Map(dest => dest.HostId, src => src.HostId.Value)
            .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(d => d.Value))
            .Map(dest => dest.MenuReviewIds, src => src.MenuReviewsIds.Select(d => d.Value));

        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Items, src => src.Items.Select(i => i.Id.Value));

        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<(CreateMenueRequest Request, string HostId), CreateMenueCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);
    }
}