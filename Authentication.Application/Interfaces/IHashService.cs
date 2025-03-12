namespace Authentication.Application.Interfaces;
public interface IHashService
{
    string GenerateHash(string data);
    bool VerifyHash(string data, string hash);
}
