namespace Gdm.Domain;

public sealed class Vault
{
    private readonly List<VaultEntry> _entries;

    public Vault(IEnumerable<VaultEntry>? entries = null)
    {
        _entries = entries?.ToList() ?? new List<VaultEntry>();
    }

    public IReadOnlyCollection<VaultEntry> Entries => _entries.AsReadOnly();

    public VaultEntry AddEntry(
        string serviceName,
        string login,
        string password,
        string? note,
        DateTimeOffset now)
    {
        var entry = new VaultEntry(EntryId.New(), serviceName, login, password, note, now, now);
        _entries.Add(entry);
        return entry;
    }

    public VaultEntry GetRequired(EntryId id)
    {
        var entry = _entries.SingleOrDefault(x => x.Id == id);
        if (entry is null)
        {
            throw new DomainException("Entry not found.");
        }

        return entry;
    }

    public IReadOnlyList<VaultEntry> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return _entries.OrderBy(x => x.ServiceName).ToList();
        }

        var normalized = query.Trim();
        return _entries
            .Where(x => x.ServiceName.Contains(normalized, StringComparison.OrdinalIgnoreCase)
                || x.Login.Contains(normalized, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.ServiceName)
            .ToList();
    }
}
