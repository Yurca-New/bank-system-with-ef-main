using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class SilverAccount : BankAccount
{
    public const int SilverDepositCostPerPoint = 5;
    public const int SilverWithdrawCostPerPoint = 2;
    public const int SilverBalanceCostPerPoint = 100;

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => 2 * this.BonusPoints;

    protected override int CalculateDepositRewardPoints(decimal amount) => Math.Max(
        (int)Math.Floor((this.Balance / SilverBalanceCostPerPoint) + (amount / SilverDepositCostPerPoint)),
        0);

    protected override int CalculateWithdrawRewardPoints(decimal amount) => Math.Max(
        (int)Math.Floor((this.Balance / SilverBalanceCostPerPoint) + (amount / SilverWithdrawCostPerPoint)),
        0);
}