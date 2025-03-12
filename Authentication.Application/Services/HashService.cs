using Authentication.Application.Interfaces;

namespace Authentication.Application.Services;
public class HashService : IHashService
{
    public string GenerateHash(string data)
    {
        return BCrypt.Net.BCrypt.HashPassword(data, BCrypt.Net.BCrypt.GenerateSalt());
    }

    public bool VerifyHash(string data, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(data, hash);
    }
}
