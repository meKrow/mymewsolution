using ExchangeRateUpdater.Repository.Integrations;
using ExchangeRateUpdater.Repository.Models;

namespace KieronRow.ExchangeRateUpdater.Providers
{
    public abstract class BaseRateProvider : IRateProvider
    {
        protected IApiRepo _exchangeRateApiRepo;
        public BaseRateProvider(IApiRepo exchangeRateApiRepo)
        {
            _exchangeRateApiRepo = exchangeRateApiRepo;
        }

        public abstract Task<List<ExchangeRate>> GetExchangeRates(IEnumerable<Currency> currencies);

        protected List<ExchangeRate> FilterResults(List<ExchangeRate> results, IEnumerable<Currency> currencies)
        {
            var currenciesAsHashset = currencies.ToRateHashSet();

            var returnValues = results.Where(x => currenciesAsHashset.Contains(x.TargetCurrency.Code)).ToList();

            return returnValues;
        }

        static bool defaultValidate(Currency val)
        {
            return val.Code.Length != 3;
        }

        Func<Currency, bool> defaultValidateMethod = new Func<Currency, bool>(defaultValidate);

        public void OverideValidateMethod(Func<Currency, bool> validateMethod)
        {
            defaultValidateMethod = validateMethod;
        }

        protected bool ValidateCurrencies(IEnumerable<Currency> currencies)
        {
            if (currencies == null)
                return false;

            if (currencies.Any() == false)
                return false;

            if (currencies.Any(x => defaultValidateMethod(x)))
                return false;

            return true;
        }
    }
}
