using ExchangeRateUpdater.Repository.Models;

namespace KieronRow.ExchangeRateUpdater
{
    internal static class Extensions
    {
        internal static HashSet<string> ToRateHashSet(this IEnumerable<Currency> currencies)
        {
            HashSet<string> strings = new HashSet<string>();

            foreach (var currency in currencies)
            {
                strings.Add(currency.Code);
            };
            return strings;
        }
    }
}
