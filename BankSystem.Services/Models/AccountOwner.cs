using BankSystem.Services.Helpers;

namespace BankSystem.Services.Models;

public class AccountOwner
{
    private string firstName;
    private string lastName;
    private string email;
    private readonly IList<Accounts.BankAccount> accounts = new List<Accounts.BankAccount>();

    public AccountOwner(string firstName, string lastName, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    public string FirstName
    {
        get => this.firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A name is required.", nameof(value));
            }

            this.firstName = value;
        }
    }

    public string LastName
    {
        get => this.lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A name is required.", nameof(value));
            }

            this.lastName = value;
        }
    }

    public string Email
    {
        get => this.email;
        set
        {
            if (!value.IsEmailValid())
            {
                throw new ArgumentException("A valid email is required.", nameof(value));
            }

            this.email = value;
        }
    }

    public void Add(Accounts.BankAccount account) => this.accounts.Add(account ?? throw new ArgumentNullException(nameof(account)));
    public IList<Accounts.BankAccount> Accounts() => this.accounts;
    public override string ToString() => $"{this.FirstName} {this.LastName}, {this.Email}.";
}