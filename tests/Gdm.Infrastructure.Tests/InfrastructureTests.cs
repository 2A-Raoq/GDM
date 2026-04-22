using Xunit;

using Gdm.Application;
using Gdm.Infrastructure;

namespace Gdm.Infrastructure.Tests;

public sealed class InfrastructureTests
{
    [Fact]
    public void Crypto_RoundTrip_ShouldWork()
    {
        var crypto = new AesGcmCryptoService();
        var payload = crypto.Encrypt("SuperSecret!42", "hello"u8.ToArray());
        var plain = crypto.Decrypt("SuperSecret!42", payload);

        Assert.Equal("hello", System.Text.Encoding.UTF8.GetString(plain));
    }

    [Fact]
    public async Task Repository_SaveAndLoad_ShouldWork()
    {
        var path = Path.Combine(Path.GetTempPath(), $"gdm-{Guid.NewGuid():N}.json");
        var repo = new FileVaultRepository(path);
        var document = new EncryptedVaultDocument(
            new EncryptedPayload("kdf", 1, "s", "n", "c", "t", 1));

        await repo.SaveAsync(document, CancellationToken.None);
        var loaded = await repo.LoadAsync(CancellationToken.None);

        Assert.NotNull(loaded);
        File.Delete(path);
    }

    [Fact]
    public void PasswordGenerator_ShouldRespectLengthAndSets()
    {
        var generator = new PasswordGenerator();
        var password = generator.Generate(new PasswordPolicy(20, true, true, true, true));

        Assert.Equal(20, password.Length);
        Assert.Contains(password, char.IsLower);
        Assert.Contains(password, char.IsUpper);
        Assert.Contains(password, char.IsDigit);
    }
}
