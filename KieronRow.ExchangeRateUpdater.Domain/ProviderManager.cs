using ExchangeRateUpdater.Repository.Integrations;
using KieronRow.ExchangeRateUpdater.Providers;
using Microsoft.Extensions.Configuration;

namespace KieronRow.ExchangeRateUpdater
{
    public class ExchangeRateProviderManager
    {
        private IApiService _apiService;
        private IConfiguration _config;

        public ExchangeRateProviderManager(IConfiguration config, IApiService apiService)
        {
            _apiService = apiService;
            _config = config;
        }

        private string GetProviderTypeName => _config["Providers:UseExchangeProvider"];
        private string GetProviderName => _config[$"Providers:{GetProviderTypeName}:Provider"];
        private string GetProviderRepoName => _config[$"Providers:{GetProviderTypeName}:Repo"];

        private IApiRepo GetProviderRepo()
        {
            Type t = Type.GetType(GetProviderRepoName);

            var repo = (IApiRepo)Activator.CreateInstance(t, _apiService);

            if (repo == null)
                throw new Exception($"Failed to create repo: {GetProviderRepoName}");

            return repo;
        }

        private IRateProvider GetProvider()
        {
            var repo = GetProviderRepo();
            Type t = Type.GetType(GetProviderName);
            var provider = (IRateProvider)Activator.CreateInstance(t, repo);

            if (provider == null)
                throw new Exception($"Failed to create provider: {GetProviderName}");

            return provider;
        }

        public IRateProvider GetExchangeRateProvider()
        {
            return GetProvider();
        }
    }
}
