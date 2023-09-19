using ExchangeRateUpdater.Repository.Exceptions;
using ExchangeRateUpdater.Repository.Models;

namespace ExchangeRateUpdater.Repository.Integrations.CzechBank
{
    public class CzecBankRepo : IApiRepo
    {
        IApiService _apiService;
 
        public CzecBankRepo(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<T> GetExchangeRates<T>(IEnumerable<Currency> currencies)
        {
            if (currencies == null || !currencies.Any())
                throw new MissingCurrencies();

            var apiModel = await _apiService.CallAPI<T>(UrlPath);

            return apiModel;
        }

        private string UrlPath => $"https://api.cnb.cz/cnbapi/exrates/daily?date={GetDateForRates}&lang=EN";
        private string GetDateForRates => DateForRates.ToString("yyyy-MM-dd");

        private DateOnly DateForRates = DateOnly.FromDateTime(DateTime.Now);
    }
}