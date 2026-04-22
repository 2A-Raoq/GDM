namespace Gdm.Domain;

public sealed class VaultEntry
{
    public VaultEntry(
        EntryId id,
        string serviceName,
        string login,
        string password,
        string? note,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        Id = id;
        ServiceName = Require(serviceName, nameof(serviceName));
        Login = Require(login, nameof(login));
        Password = Require(password, nameof(password));
        Note = note?.Trim();
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public EntryId Id { get; }

    public string ServiceName { get; private set; }

    public string Login { get; private set; }

    public string Password { get; private set; }

    public string? Note { get; private set; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public void Update(string serviceName, string login, string password, string? note, DateTimeOffset updatedAt)
    {
        ServiceName = Require(serviceName, nameof(serviceName));
        Login = Require(login, nameof(login));
        Password = Require(password, nameof(password));
        Note = note?.Trim();
        UpdatedAt = updatedAt;
    }

    private static string Require(string value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"{field} is required.");
        }

        return value.Trim();
    }
}
