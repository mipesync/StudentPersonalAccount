using StudentPersonalAccount.IBCrypt;

namespace StudentPersonalAccount.Classes;

public class BCryption : IEncryption
{
    public string Encrypt(string _string)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(_string);
        return hash;
    }

    public bool Verify(string _string, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(_string, hash);
    }
}