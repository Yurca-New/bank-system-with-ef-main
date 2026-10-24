using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class BasedOnTickUniqueNumberGenerator(DateTime startingPoint) : IUniqueNumberGenerator
{
    public string Generate() => (DateTime.UtcNow.Ticks - startingPoint.Ticks).ToString().GenerateHash();
}