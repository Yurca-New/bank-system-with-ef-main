using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public sealed class SimpleGenerator : IUniqueNumberGenerator
{
    private long lastNumber = 1234567890;

    private SimpleGenerator()
    {
    }

    public static SimpleGenerator Instance { get; } = new();

    public string Generate() => (++this.lastNumber).ToString().GenerateHash();
}
