using ExchangeRateUpdater.Repository.Models;

namespace ExchangeRateUpdater.Repository.Integrations
{
    public interface IApiRepo
    {
        Task<T> GetExchangeRates<T>(IEnumerable<Currency> currencies);
    }
}