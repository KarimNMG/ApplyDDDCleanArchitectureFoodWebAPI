﻿using Project.Domain.UserAggregate;

namespace Project.Application.Common.Interfaces.Presistance;

public interface IUserRepository
{
    User? GetUserByEmailAsync(string email);
    Guid AddAsync(User user);
}
