using Project.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Menus.Queries.GetAllMenus;

public sealed record GetAllMenusQuery() : ICachedQuery<List<GetAllMenusQueryResponse>>
{
    string ICachedQuery.CacheKey => $"get-all-menus";
    TimeSpan? ICachedQuery.Expiration => null;
}