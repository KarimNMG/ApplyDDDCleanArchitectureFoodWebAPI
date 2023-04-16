using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Menus;

public sealed record CreateMenueRequest(
    string Name,
    string Description,
    List<MenuSectionRequest> Sections);

public sealed record MenuSectionRequest(
    string Name,
    string Description,
    List<MenuItemRequest> Items
    );

public sealed record MenuItemRequest(
    string Name,
    string Description);