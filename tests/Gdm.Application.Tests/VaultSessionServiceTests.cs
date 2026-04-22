using Xunit;

using System.Text;
using Gdm.Application;

namespace Gdm.Application.Tests;

public sealed class VaultSessionServiceTests
{
    [Fact]
    public async Task CreateUnlockAndAddEntry_ShouldPersist()
    {
        var repo = new InMemoryRepository();
        var service = new VaultSessionService(repo, new FakeCrypto(), new FakeClock());

        await service.CreateVaultAsync("SuperSecret!42", CancellationToken.None);
        service.Lock();
        await service.UnlockAsync("SuperSecret!42", CancellationToken.None);

        var created = await service.AddEntryAsync("GitHub", "alice", "pwd", "note", CancellationToken.None);
        var loaded = service.GetEntry(created.Id);

        Assert.Equal("GitHub", loaded.ServiceName);
    }

    [Fact]
    public async Task Unlock_WithWrongPassword_ShouldFail()
    {
        var repo = new InMemoryRepository();
        var service = new VaultSessionService(repo, new FakeCrypto(), new FakeClock());

        await service.CreateVaultAsync("SuperSecret!42", CancellationToken.None);
        service.Lock();

        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => service.UnlockAsync("bad-password", CancellationToken.None));
    }

    private sealed class InMemoryRepository : IVaultRepository
    {
        private EncryptedVaultDocument? _doc;

        public Task SaveAsync(EncryptedVaultDocument document, CancellationToken cancellationToken)
        {
            _doc = document;
            return Task.CompletedTask;
        }

        public Task<EncryptedVaultDocument?> LoadAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_doc);
        }
    }

    private sealed class FakeCrypto : ICryptoService
    {
        public EncryptedPayload Encrypt(string masterPassword, byte[] plaintext)
        {
            return new EncryptedPayload(
                "fake",
                1,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(masterPassword)),
                "",
                Convert.ToBase64String(plaintext),
                "",
                1);
        }

        public byte[] Decrypt(string masterPassword, EncryptedPayload payload)
        {
            var expected = Encoding.UTF8.GetString(Convert.FromBase64String(payload.Salt));
            if (!string.Equals(masterPassword, expected, StringComparison.Ordinal))
            {
                throw new UnauthorizedAccessException("Bad password");
            }

            return Convert.FromBase64String(payload.Ciphertext);
        }
    }

    private sealed class FakeClock : IDateTimeProvider
    {
        public DateTimeOffset UtcNow => new(2026, 4, 1, 10, 0, 0, TimeSpan.Zero);
    }
}
