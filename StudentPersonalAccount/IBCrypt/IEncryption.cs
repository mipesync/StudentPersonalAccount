namespace StudentPersonalAccount.IBCrypt;

public interface IEncryption
{
    string Encrypt(string _string);
    bool Verify(string _string, string hash);
}