using Microsoft.AspNetCore.DataProtection;

namespace Yana.Infrastructure.Services.EncryptionService;

public sealed class EncryptionService : IEncryptionService
{
    private readonly IDataProtector _dataProtector;

    public EncryptionService(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtector = dataProtectionProvider.CreateProtector("EncryptionProtector");
    }

    public string Encrypt(string value)
        => _dataProtector.Protect(value);

    public string? Decrypt(string encryptedValue)
        => _dataProtector.Unprotect(encryptedValue);
}