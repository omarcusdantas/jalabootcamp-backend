namespace MyWalletAPI.Models;

public abstract class Currency(string name)
{
    public string Name { get; } = name;
}

public class Dollar : Currency
{
    public Dollar() : base(nameof(Dollar)) { }
}

public class Euro : Currency
{
    public Euro() : base(nameof(Euro)) { }
}
