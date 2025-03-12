using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;
public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<bool> UserExistsByEmailAsync(string email);
}
