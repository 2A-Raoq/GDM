using Gdm.Domain;

namespace Gdm.Application;

public interface IVaultRepository
{
    Task SaveAsync(EncryptedVaultDocument document, CancellationToken cancellationToken);

    Task<EncryptedVaultDocument?> LoadAsync(CancellationToken cancellationToken);
}

public interface ICryptoService
{
    EncryptedPayload Encrypt(string masterPassword, byte[] plaintext);

    byte[] Decrypt(string masterPassword, EncryptedPayload payload);
}

public interface IPasswordGenerator
{
    string Generate(PasswordPolicy policy);
}

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}

public sealed record PasswordPolicy(int Length, bool Lowercase, bool Uppercase, bool Digits, bool Symbols)
{
    public static PasswordPolicy Default => new(16, true, true, true, true);
}

public sealed record EncryptedPayload(
    string Kdf,
    int Iterations,
    string Salt,
    string Nonce,
    string Ciphertext,
    string Tag,
    int Version);

public sealed record EncryptedVaultDocument(EncryptedPayload Payload);

public sealed record VaultEntryDto(
    string Id,
    string ServiceName,
    string Login,
    string Password,
    string? Note,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);

public sealed record VaultStateDto(bool Exists, bool IsUnlocked, IReadOnlyList<VaultEntryDto> Entries);
