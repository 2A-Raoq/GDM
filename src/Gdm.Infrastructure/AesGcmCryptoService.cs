using System.Security.Cryptography;
using Gdm.Application;

namespace Gdm.Infrastructure;

public sealed class AesGcmCryptoService : ICryptoService
{
    private const int KeySize = 32;
    private const int SaltSize = 16;
    private const int NonceSize = 12;
    private const int TagSize = 16;
    private const int Iterations = 210_000;

    public EncryptedPayload Encrypt(string masterPassword, byte[] plaintext)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var nonce = RandomNumberGenerator.GetBytes(NonceSize);
        var key = DeriveKey(masterPassword, salt, Iterations);
        var ciphertext = new byte[plaintext.Length];
        var tag = new byte[TagSize];

        using var aes = new AesGcm(key, TagSize);
        aes.Encrypt(nonce, plaintext, ciphertext, tag);

        return new EncryptedPayload(
            "PBKDF2-SHA256",
            Iterations,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(nonce),
            Convert.ToBase64String(ciphertext),
            Convert.ToBase64String(tag),
            1);
    }

    public byte[] Decrypt(string masterPassword, EncryptedPayload payload)
    {
        var salt = Convert.FromBase64String(payload.Salt);
        var nonce = Convert.FromBase64String(payload.Nonce);
        var ciphertext = Convert.FromBase64String(payload.Ciphertext);
        var tag = Convert.FromBase64String(payload.Tag);
        var key = DeriveKey(masterPassword, salt, payload.Iterations);
        var plaintext = new byte[ciphertext.Length];

        using var aes = new AesGcm(key, TagSize);
        try
        {
            aes.Decrypt(nonce, ciphertext, tag, plaintext);
        }
        catch (CryptographicException ex)
        {
            throw new UnauthorizedAccessException("Incorrect password or corrupted vault.", ex);
        }

        return plaintext;
    }

    private static byte[] DeriveKey(string masterPassword, byte[] salt, int iterations)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            masterPassword,
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            KeySize);
    }
}
