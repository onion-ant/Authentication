using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Interfaces;
public interface IEmailRepository
{
    Task<EmailVerificationToken> CreateEmailTokenAsync(EmailVerificationToken verificationToken);
    Task DeleteEmailTokenAsync(EmailVerificationToken verificationToken);
    Task<EmailVerificationToken?> GetEmailTokenAsync(Guid token);
}
