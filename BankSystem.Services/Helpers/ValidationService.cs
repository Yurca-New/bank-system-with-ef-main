using System.Globalization;
using System.Net.Mail;

namespace BankSystem.Services.Helpers;

public static class ValidationService
{
    public static bool IsCurrencyValid(this string currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            return false;
        }

        return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(culture => new RegionInfo(culture.Name).ISOCurrencySymbol)
            .Contains(currencyCode, StringComparer.OrdinalIgnoreCase);
    }

    public static bool IsEmailValid(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var address = new MailAddress(email);
            return address.Address == email;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}