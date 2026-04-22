using System.Security.Cryptography;
using System.Text;
using Gdm.Application;

namespace Gdm.Infrastructure;

public sealed class PasswordGenerator : IPasswordGenerator
{
    private const string Lower = "abcdefghijklmnopqrstuvwxyz";
    private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Symbols = "!@#$%^&*()-_=+[]{};:,.?";

    public string Generate(PasswordPolicy policy)
    {
        if (policy.Length < 8)
        {
            throw new ArgumentException("Password length must be at least 8.");
        }

        var sets = new List<string>();
        if (policy.Lowercase) sets.Add(Lower);
        if (policy.Uppercase) sets.Add(Upper);
        if (policy.Digits) sets.Add(Digits);
        if (policy.Symbols) sets.Add(Symbols);

        if (sets.Count == 0)
        {
            throw new ArgumentException("At least one character set must be selected.");
        }

        var all = string.Concat(sets);
        var chars = new List<char>(policy.Length);

        foreach (var set in sets)
        {
            chars.Add(set[RandomNumberGenerator.GetInt32(set.Length)]);
        }

        while (chars.Count < policy.Length)
        {
            chars.Add(all[RandomNumberGenerator.GetInt32(all.Length)]);
        }

        Shuffle(chars);
        return new string(chars.ToArray());
    }

    private static void Shuffle(IList<char> chars)
    {
        for (var i = chars.Count - 1; i > 0; i--)
        {
            var j = RandomNumberGenerator.GetInt32(i + 1);
            (chars[i], chars[j]) = (chars[j], chars[i]);
        }
    }
}
