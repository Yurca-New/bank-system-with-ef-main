using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class GuidNumberGenerator : IUniqueNumberGenerator
{
    public string Generate() => Guid.NewGuid().ToString().GenerateHash();
}