using System.Globalization;

namespace BankSystem.Services.Models;

public class AccountCashOperation(decimal amount, DateTime date, string note)
{
    public decimal Amount { get; } = amount;
    public DateTime Date { get; } = date;
    public string Note { get; } = note;

    public override string ToString()
    {
        string operation = this.Amount < 0 ? "Debited from account" : "Credited to account";
        return string.Format(
            CultureInfo.InvariantCulture,
            "{0:MM/dd/yyyy HH:mm:ss} {1} : {2} {3}.",
            this.Date,
            this.Note,
            operation,
            this.Amount);
    }

}