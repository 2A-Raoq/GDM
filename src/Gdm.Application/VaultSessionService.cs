using System.Text.Json;
using Gdm.Domain;

namespace Gdm.Application;

public sealed class VaultSessionService
{
    private readonly IVaultRepository _repository;
    private readonly ICryptoService _crypto;
    private readonly IDateTimeProvider _clock;
    private Vault? _vault;
    private string? _masterPassword;

    public VaultSessionService(IVaultRepository repository, ICryptoService crypto, IDateTimeProvider clock)
    {
        _repository = repository;
        _crypto = crypto;
        _clock = clock;
    }

    public bool IsUnlocked => _vault is not null;

    public async Task CreateVaultAsync(string masterPassword, CancellationToken cancellationToken)
    {
        ValidateMasterPassword(masterPassword);
        _vault = new Vault();
        _masterPassword = masterPassword;
        await SaveAsync(cancellationToken);
    }

    public async Task UnlockAsync(string masterPassword, CancellationToken cancellationToken)
    {
        var document = await _repository.LoadAsync(cancellationToken);
        if (document is null)
        {
            throw new InvalidOperationException("Vault not found.");
        }

        var bytes = _crypto.Decrypt(masterPassword, document.Payload);
        var snapshot = JsonSerializer.Deserialize<VaultSnapshot>(bytes)
            ?? throw new InvalidOperationException("Vault is corrupted.");
        _vault = snapshot.ToDomain();
        _masterPassword = masterPassword;
    }

    public void Lock()
    {
        _vault = null;
        _masterPassword = null;
    }

    public async Task ChangeMasterPasswordAsync(
        string currentMasterPassword,
        string newMasterPassword,
        CancellationToken cancellationToken)
    {
        EnsureUnlocked();
        if (!string.Equals(_masterPassword, currentMasterPassword, StringComparison.Ordinal))
        {
            throw new UnauthorizedAccessException("Incorrect master password.");
        }

        ValidateMasterPassword(newMasterPassword);
        _masterPassword = newMasterPassword;
        await SaveAsync(cancellationToken);
    }

    public async Task<VaultEntryDto> AddEntryAsync(
        string serviceName,
        string login,
        string password,
        string? note,
        CancellationToken cancellationToken)
    {
        var vault = EnsureUnlocked();
        var created = vault.AddEntry(serviceName, login, password, note, _clock.UtcNow);
        await SaveAsync(cancellationToken);
        return Map(created);
    }

    public async Task<VaultEntryDto> UpdateEntryAsync(
        string id,
        string serviceName,
        string login,
        string password,
        string? note,
        CancellationToken cancellationToken)
    {
        var vault = EnsureUnlocked();
        var entryId = ParseId(id);
        var entry = vault.GetRequired(entryId);
        entry.Update(serviceName, login, password, note, _clock.UtcNow);
        await SaveAsync(cancellationToken);
        return Map(entry);
    }

    public IReadOnlyList<VaultEntryDto> Search(string query)
    {
        var vault = EnsureUnlocked();
        return vault.Search(query).Select(Map).ToList();
    }

    public VaultEntryDto GetEntry(string id)
    {
        var vault = EnsureUnlocked();
        return Map(vault.GetRequired(ParseId(id)));
    }

    public async Task<bool> ExistsAsync(CancellationToken cancellationToken)
    {
        return await _repository.LoadAsync(cancellationToken) is not null;
    }

    private async Task SaveAsync(CancellationToken cancellationToken)
    {
        var vault = EnsureUnlocked();
        var password = _masterPassword ?? throw new InvalidOperationException("Vault is locked.");
        var snapshot = VaultSnapshot.FromDomain(vault);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(snapshot);
        var payload = _crypto.Encrypt(password, bytes);
        await _repository.SaveAsync(new EncryptedVaultDocument(payload), cancellationToken);
    }

    private Vault EnsureUnlocked()
    {
        return _vault ?? throw new InvalidOperationException("Vault is locked.");
    }

    private static void ValidateMasterPassword(string masterPassword)
    {
        if (string.IsNullOrWhiteSpace(masterPassword) || masterPassword.Trim().Length < 10)
        {
            throw new ArgumentException("Master password must contain at least 10 characters.");
        }
    }

    private static EntryId ParseId(string id)
    {
        if (!Guid.TryParse(id, out var value))
        {
            throw new ArgumentException("Invalid entry id.");
        }

        return new EntryId(value);
    }

    private static VaultEntryDto Map(VaultEntry entry)
    {
        return new VaultEntryDto(
            entry.Id.ToString(),
            entry.ServiceName,
            entry.Login,
            entry.Password,
            entry.Note,
            entry.CreatedAt,
            entry.UpdatedAt);
    }

    private sealed record VaultSnapshot(IReadOnlyList<VaultEntrySnapshot> Entries)
    {
        public static VaultSnapshot FromDomain(Vault vault)
        {
            return new VaultSnapshot(vault.Entries.Select(VaultEntrySnapshot.FromDomain).ToList());
        }

        public Vault ToDomain()
        {
            return new Vault(Entries.Select(x => x.ToDomain()));
        }
    }

    private sealed record VaultEntrySnapshot(
        string Id,
        string ServiceName,
        string Login,
        string Password,
        string? Note,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt)
    {
        public static VaultEntrySnapshot FromDomain(VaultEntry entry)
        {
            return new VaultEntrySnapshot(
                entry.Id.ToString(),
                entry.ServiceName,
                entry.Login,
                entry.Password,
                entry.Note,
                entry.CreatedAt,
                entry.UpdatedAt);
        }

        public VaultEntry ToDomain()
        {
            return new VaultEntry(
                new EntryId(Guid.Parse(Id)),
                ServiceName,
                Login,
                Password,
                Note,
                CreatedAt,
                UpdatedAt);
        }
    }
}
