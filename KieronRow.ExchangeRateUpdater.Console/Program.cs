using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExchangeRateUpdater.Repository.Integrations.CzechBank;
using ExchangeRateUpdater.Repository.Models;
using ExchangeRateUpdater.Repository.Services;
using KieronRow.ExchangeRateUpdater;
using KieronRow.ExchangeRateUpdater.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeRateUpdater
{
    public static class Program
    {
        private static IConfiguration _config;

        private static IEnumerable<Currency> testCurrencies = new[]
        {
            new Currency("USD"),
            new Currency("EUR"),
            new Currency("CZK"),
            new Currency("JPY"),
            new Currency("KES"),
            new Currency("RUB"),
            new Currency("THB"),
            new Currency("TRY"),
            new Currency("XYZ")
        };

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IApiService, ApiService>()
                            .AddSingleton<HttpClient, HttpClient>()
                            .AddSingleton<ExchangeRateProviderManager, ExchangeRateProviderManager>());
        }

        private static IServiceProvider Startup(string[] args)
        {
            //setup the configuration
            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false);

            _config = builder.Build();

            //setup the DI
            var host = CreateHostBuilder(args).Build();

            return host.Services;
        }

        //There are two providers, both actually do pretty much the same thing, but I wanted to be able to easily add a new provider
        //if needed, (i.e. the bank was to retrieve it's exchange-rates from a different source).  This selection of provider class
        //is controlled within the app-settings.  In theory you could also configure a different repo as this is where the logic
        //is held to actually make the external calls to an API.
        public static async Task Main(string[] args)
        {
            try
            {
                var svc = Startup(args);

                var exchangeRateProviderManager = svc.GetService<ExchangeRateProviderManager>();

                var provider = exchangeRateProviderManager.GetExchangeRateProvider();

                var rates = await provider.GetExchangeRates(testCurrencies);

                Console.WriteLine($"Successfully retrieved {rates.Count} exchange rates:");
                foreach (var rate in rates)
                {
                    Console.WriteLine(rate.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not retrieve exchange rates: '{e.Message}'.");
            }

            Console.ReadLine();
        }
    }
}
