using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;
public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<User> CreateUserAsync(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task VerifyUserEmailAsync(int userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        user.Verified = true;
        await _dbContext.SaveChangesAsync();
    }
}
