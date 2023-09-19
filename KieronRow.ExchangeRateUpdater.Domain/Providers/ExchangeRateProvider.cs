using ExchangeRateUpdater.Repository.Exceptions;
using ExchangeRateUpdater.Repository.Integrations;
using ExchangeRateUpdater.Repository.Integrations.CzechBank;
using ExchangeRateUpdater.Repository.Models;

namespace KieronRow.ExchangeRateUpdater.Providers
{
    public class ExchangeRateProvider : BaseRateProvider
    {
        const string fromCurrency = "CZK";

        public ExchangeRateProvider(IApiRepo exchangeRateApiRepo) : base(exchangeRateApiRepo) {}

        /// <summary>
        /// Should return exchange rates among the specified currencies that are defined by the source. But only those defined
        /// by the source, do not return calculated exchange rates. E.g. if the source contains "CZK/USD" but not "USD/CZK",
        /// do not return exchange rate "USD/CZK" with value calculated as 1 / "CZK/USD". If the source does not provide
        /// some of the currencies, ignore them.
        /// </summary>
        public override async Task<List<ExchangeRate>> GetExchangeRates(IEnumerable<Currency> currencies)
        {
            if (ValidateCurrencies(currencies))
            {
                var apiRates = await _exchangeRateApiRepo.GetExchangeRates<CzecBankModel>(currencies);

                if (apiRates != null && apiRates.rates.Any())
                {
                    var mappedRates = MapExchangeRateModel(apiRates);
                    var filteredExchangeRates = FilterResults(mappedRates, currencies);
                   
                    return filteredExchangeRates;
                }
            }
            else
            {
                throw new InvalidCurrencyException();
            }

            return new List<ExchangeRate>();
        }

        private List<ExchangeRate> MapExchangeRateModel(CzecBankModel apiModel)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

            foreach (var r in apiModel.rates)
            {
                exchangeRates.Add(new ExchangeRate(new Currency(fromCurrency), new Currency(r.currencyCode), r.rate));
            }

            return exchangeRates;
        }
    }
}
