using ExchangeRateUpdater.Repository.Models;

namespace KieronRow.ExchangeRateUpdater.Providers
{
    public interface IRateProvider
    {
        Task<List<ExchangeRate>> GetExchangeRates(IEnumerable<Currency> currencies);
    }
}