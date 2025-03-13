using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;
public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task VerifyUserEmailAsync(int userId);
}
