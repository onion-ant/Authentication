using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Infrastructure.Repositories;
public class EmailRepository(AppDbContext dbContext) : IEmailRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<EmailVerificationToken> CreateEmailTokenAsync(EmailVerificationToken verificationToken)
    {
        await _dbContext.EmailVerificationTokens.AddAsync(verificationToken);
        await _dbContext.SaveChangesAsync();
        return verificationToken;
    }

    public async Task DeleteEmailTokenAsync(EmailVerificationToken verificationToken)
    {
        _dbContext.EmailVerificationTokens.Remove(verificationToken);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<EmailVerificationToken?> GetEmailTokenAsync(Guid token)
    {
        return await _dbContext.EmailVerificationTokens.FindAsync(token);
    }
}
