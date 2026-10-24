using System.Globalization;
using BankSystem.Services.Generators;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Models.Accounts;

public abstract class BankAccount
{
    private readonly IList<AccountCashOperation> operations = new List<AccountCashOperation>();

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
    : this(
        owner,
        currencyCode,
        uniqueNumberGenerator is null ? throw new ArgumentNullException(nameof(uniqueNumberGenerator)) : uniqueNumberGenerator.Generate)
    {
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
    {
        this.AccountOwner = owner ?? throw new ArgumentNullException(nameof(owner));
        if (!currencyCode.IsCurrencyValid())
        {
            throw new ArgumentException("An ISO currency code is required.", nameof(currencyCode));
        }

        this.CurrencyCode = currencyCode;
        this.Number = (numberGenerator ?? throw new ArgumentNullException(nameof(numberGenerator)))();
        owner.Add(this);
    }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
    : this(owner, currencyCode, uniqueNumberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : this(owner, currencyCode, numberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    public string Number { get; }

    public decimal Balance { get; private set; }

    public string CurrencyCode { get; }

    public AccountOwner AccountOwner { get; }

    public int BonusPoints { get; private set; }

    protected abstract decimal Overdraft { get; }

    public void Deposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        this.Balance += amount;
        this.operations.Add(new AccountCashOperation(amount, date, note));
        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (amount > this.Balance + this.Overdraft)
        {
            throw new InvalidOperationException("The withdrawal exceeds the available balance.");
        }

        this.Balance -= amount;
        this.operations.Add(new AccountCashOperation(-amount, date, note));
        this.BonusPoints += this.CalculateWithdrawRewardPoints(amount);
    }

    public IList<AccountCashOperation> GetAllOperations() => this.operations;

    public override string ToString() => string.Format(
    CultureInfo.InvariantCulture,
    "{0} {1} No:{2}. Balance: {3}{4}.\n",
    this.Overdraft,
    this.AccountOwner,
    this.Number,
    this.Balance,
    this.CurrencyCode);

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);
}