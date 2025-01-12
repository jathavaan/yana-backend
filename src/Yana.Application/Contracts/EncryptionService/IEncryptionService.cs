namespace Yana.Application.Contracts.EncryptionService;

public interface IEncryptionService
{
    public string Encrypt(string value);
    public string? Decrypt(string encryptedValue);
}