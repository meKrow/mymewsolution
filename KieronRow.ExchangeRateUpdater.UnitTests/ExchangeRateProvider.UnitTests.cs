using ExchangeRateUpdater.Repository.Exceptions;
using ExchangeRateUpdater.Repository.Integrations;
using ExchangeRateUpdater.Repository.Integrations.CzechBank;
using ExchangeRateUpdater.Repository.Models;
using KieronRow.ExchangeRateUpdater.Providers;
using Moq;

namespace KieronRow.ExchangeRateUpdaterProvider.UnitTests
{
    public class ExchangeRateProviderUnitTests
    {
        [Fact]
        public async Task Test_Filtering()
        {
            Mock<IApiRepo> mockRepo = new Mock<IApiRepo>();

            IEnumerable<Currency> inputCurrencies = new[]
            {
                new Currency("USD"),            //<--should find
                new Currency("EUR"),            //<--should find
                new Currency("CZK"),            //<--ignore
                new Currency("JPY"),            //<--ignore
                new Currency("KES"),            //<--ignore
                new Currency("RUB"),            //<--should find
                new Currency("THB"),            //<--ignore
                new Currency("TRY"),            //<--ignore
                new Currency("XYZ")             //<--ignore
            };

            var outputStubCzecBankModel = new CzecBankModel()
            {
                rates = new List<Rate>() {
                new Rate() { currencyCode = "USD", rate = 1m },
                new Rate() { currencyCode = "EUR", rate = 1.1m },
                new Rate() { currencyCode = "RUB", rate = 19m },
                new Rate() { currencyCode = "AWG", rate = 56743m },
                new Rate() { currencyCode = "AOA", rate = 0.45475543m },
                new Rate() { currencyCode = "ARS", rate = 42.24542m },
            }
            };

            var expected = new List<ExchangeRate>()
            {
                new ExchangeRate(new Currency("CZK"),new Currency("USD"),1m),
                new ExchangeRate(new Currency("CZK"),new Currency("EUR"),1.1m),
                new ExchangeRate(new Currency("CZK"),new Currency("RUB"),19m)
            };

            mockRepo.Setup(x => x.GetExchangeRates<CzecBankModel>(It.IsAny<IEnumerable<Currency>>())).ReturnsAsync(outputStubCzecBankModel);

            ExchangeRateProvider exchangeRateProvider = new ExchangeRateProvider(mockRepo.Object);

            var actual = await exchangeRateProvider.GetExchangeRates(inputCurrencies);

            Assert.Equal(expected.Count, actual.Count);

            foreach (var a in actual)
            {
                Assert.NotNull(expected.Any(e => e.TargetCurrency == a.TargetCurrency && e.SourceCurrency == a.SourceCurrency && e.Value == a.Value));
            }
        }

        [Fact]
        public async Task Test_MappingFromApiOutputModelToInternalModel()
        {
            Mock<IApiRepo> mockRepo = new Mock<IApiRepo>();

            mockRepo.Setup(x => x.GetExchangeRates<CzecBankModel>(It.IsAny<IEnumerable<Currency>>())).ReturnsAsync(TestHelper.StubCzecBankModel);

            ExchangeRateProvider exchangeRateProvider = new ExchangeRateProvider(mockRepo.Object);

            var actual = await exchangeRateProvider.GetExchangeRates(TestHelper.TestCurrencies);

            foreach (var c in actual)
            {
                var x = TestHelper.StubCzecBankModel.rates.First(xx => xx.currencyCode == c.TargetCurrency.Code);
                Assert.NotNull(x);
                Assert.Equal(x.rate, c.Value);
            }
        }

        [Fact]
        public async Task Test_DefaultValidateMethodSuccessWithGoodCurrencies()
        {
            Mock<IApiRepo> mockRepo = new Mock<IApiRepo>();

            ExchangeRateProvider exchangeRateProvider = new ExchangeRateProvider(mockRepo.Object);

            var actual = await exchangeRateProvider.GetExchangeRates(TestHelper.TestCurrencies);

            //no data returned because the repo is not configured to return any values.
            Assert.Equal(new List<ExchangeRate>(), actual);
        }

        [Fact]
        public async Task Test_FailsDefaultValidateWithNoCurrencies()
        {
            Mock<IApiRepo> mockRepo = new Mock<IApiRepo>();

            ExchangeRateProvider exchangeRateProvider = new ExchangeRateProvider(mockRepo.Object);

            await Assert.ThrowsAsync<InvalidCurrencyException>(() => exchangeRateProvider.GetExchangeRates(null));
        }

        [Fact]
        public async Task Test_FailsDefaultValidateWithInvalidCurrencies()
        {
            Mock<IApiRepo> mockRepo = new Mock<IApiRepo>();

            ExchangeRateProvider exchangeRateProvider = new ExchangeRateProvider(mockRepo.Object);

            List<Currency> currencies = new List<Currency>()
            {
                new Currency("A"),
                new Currency("AA"),
                new Currency("AAA"),
            };

            await Assert.ThrowsAsync<InvalidCurrencyException>(() => exchangeRateProvider.GetExchangeRates(currencies));
        }

        static bool validateAlwaysSucceeds(Currency val)
        {
            return false;
        }

        [Fact]
        public async Task Test_OverrideValidation()
        {
            Mock<IApiRepo> mockRepo = new Mock<IApiRepo>();

            ExchangeRateProvider exchangeRateProvider = new ExchangeRateProvider(mockRepo.Object);

            exchangeRateProvider.OverideValidateMethod(validateAlwaysSucceeds);

            List<Currency> currencies = new List<Currency>()
            {
                new Currency("A"),
                new Currency("AA"),
                new Currency("AAA"),
            };

            var actual = await exchangeRateProvider.GetExchangeRates(TestHelper.TestCurrencies);

            Assert.Equal(new List<ExchangeRate>(), actual);
        }
    }
}