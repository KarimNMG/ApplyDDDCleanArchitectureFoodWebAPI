
using Project.Application.Common.Interfaces.Presistance;
using Project.Domain.UserAggregate;

namespace Project.Infrastructure.Presistance;
internal class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new List<User>();
    public Guid AddAsync(User user)
    {
        _users.Add(user);
        return user.Id;
    }

    public User? GetUserByEmailAsync(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}