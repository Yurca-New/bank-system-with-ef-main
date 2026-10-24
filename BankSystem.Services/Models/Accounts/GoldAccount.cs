using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class GoldAccount : BankAccount
{
    public const int GoldDepositCostPerPoint = 10;
    public const int GoldWithdrawCostPerPoint = 5;
    public const int GoldBalanceCostPerPoint = 5;

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => 3 * this.BonusPoints;

    protected override int CalculateDepositRewardPoints(decimal amount) => Math.Max(
        (int)Math.Ceiling((this.Balance / GoldBalanceCostPerPoint) + (amount / GoldDepositCostPerPoint)),
        0);

    protected override int CalculateWithdrawRewardPoints(decimal amount) => Math.Max(
        (int)Math.Ceiling((this.Balance / GoldBalanceCostPerPoint) + (amount / GoldWithdrawCostPerPoint)),
        0);
}