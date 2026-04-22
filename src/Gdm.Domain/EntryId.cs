namespace Gdm.Domain;

public readonly record struct EntryId(Guid Value)
{
    public static EntryId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("D");
}
