using System.Text.Json;
using Gdm.Application;

namespace Gdm.Infrastructure;

public sealed class FileVaultRepository : IVaultRepository
{
    private readonly string _filePath;

    public FileVaultRepository(string filePath)
    {
        _filePath = filePath;
    }

    public async Task SaveAsync(EncryptedVaultDocument document, CancellationToken cancellationToken)
    {
        var directory = Path.GetDirectoryName(_filePath)
            ?? throw new InvalidOperationException("Invalid vault path.");
        Directory.CreateDirectory(directory);

        var temp = _filePath + ".tmp";
        var bytes = JsonSerializer.SerializeToUtf8Bytes(document);
        await File.WriteAllBytesAsync(temp, bytes, cancellationToken);
        File.Move(temp, _filePath, true);
    }

    public async Task<EncryptedVaultDocument?> LoadAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        var bytes = await File.ReadAllBytesAsync(_filePath, cancellationToken);
        return JsonSerializer.Deserialize<EncryptedVaultDocument>(bytes)
            ?? throw new InvalidOperationException("Vault file corrupted.");
    }
}
