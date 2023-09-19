using ExchangeRateUpdater.Repository.Exceptions;
using ExchangeRateUpdater.Repository.Integrations.CzechBank;
using ExchangeRateUpdater.Repository.Models;
using Moq;
using Newtonsoft.Json;

namespace KieronRow.ExchangeRateUpdaterProvider.UnitTests
{
    public class CzecBankRepoUnitTests
    {
        [Fact]
        public async Task Test_IfItIsPossibleToDesialiseApiOutModel()
        {
            Assert.NotNull(JsonConvert.DeserializeObject<CzecBankModel>(TestHelper.ApiSourceJson));
        }
        
        [Fact]
        public async Task Test_RepoThrowsErrorIfCurrenciesNull()
        {
            Mock<IApiService> mockApiService = new Mock<IApiService>();

            CzecBankRepo c = new CzecBankRepo(mockApiService.Object);

            await Assert.ThrowsAsync<MissingCurrencies>(() => c.GetExchangeRates<CzecBankModel>(null));
        }

        [Fact]
        public async Task Test_RepoThrowsErrorIfCurrenciesEmpty()
        {
            Mock<IApiService> mockApiService = new Mock<IApiService>();

            CzecBankRepo c = new CzecBankRepo(mockApiService.Object);

            await Assert.ThrowsAsync<MissingCurrencies>(() => c.GetExchangeRates<CzecBankModel>(new List<Currency>()));
        }
    }
}