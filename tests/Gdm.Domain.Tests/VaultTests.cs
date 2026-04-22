using Xunit;

using Gdm.Domain;

namespace Gdm.Domain.Tests;

public sealed class VaultTests
{
    [Fact]
    public void AddEntry_ShouldRejectMissingFields()
    {
        var vault = new Vault();
        var now = DateTimeOffset.UtcNow;

        Assert.Throws<DomainException>(() => vault.AddEntry("", "login", "pass", null, now));
        Assert.Throws<DomainException>(() => vault.AddEntry("svc", "", "pass", null, now));
        Assert.Throws<DomainException>(() => vault.AddEntry("svc", "login", "", null, now));
    }

    [Fact]
    public void Search_ShouldFilterByServiceOrLogin()
    {
        var vault = new Vault();
        var now = DateTimeOffset.UtcNow;

        vault.AddEntry("GitHub", "alice", "p1", null, now);
        vault.AddEntry("Mail", "bob@example.com", "p2", null, now);

        Assert.Single(vault.Search("git"));
        Assert.Single(vault.Search("bob"));
    }
}
