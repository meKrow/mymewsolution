using ExchangeRateUpdater.Repository.Integrations.CzechBank;
using ExchangeRateUpdater.Repository.Models;

namespace KieronRow.ExchangeRateUpdaterProvider.UnitTests
{
    public class TestHelper
    {
        internal static string ApiSourceJson = "{\r\n \"result\":\"success\",\r\n \"documentation\":\"https://www.exchangerate-api.com/docs\",\r\n \"terms_of_use\":\"https://www.exchangerate-api.com/terms\",\r\n \"time_last_update_unix\":1694736002,\r\n \"time_last_update_utc\":\"Fri, 15 Sep 2023 00:00:02 +0000\",\r\n \"time_next_update_unix\":1694822402,\r\n \"time_next_update_utc\":\"Sat, 16 Sep 2023 00:00:02 +0000\",\r\n \"base_code\":\"USD\",\r\n \"conversion_rates\":{\r\n  \"USD\":1,\r\n  \"AED\":3.6725,\r\n  \"AFN\":78.9946,\r\n  \"ALL\":99.1552,\r\n  \"AMD\":386.2562,\r\n  \"ANG\":1.7900,\r\n  \"AOA\":833.3350,\r\n  \"ARS\":349.9800,\r\n  \"AUD\":1.5537,\r\n  \"AWG\":1.7900,\r\n  \"AZN\":1.6987,\r\n  \"BAM\":1.8338,\r\n  \"BBD\":2.0000,\r\n  \"BDT\":109.7647,\r\n  \"BGN\":1.8326,\r\n  \"BHD\":0.3760,\r\n  \"BIF\":2827.7603,\r\n  \"BMD\":1.0000,\r\n  \"BND\":1.3626,\r\n  \"BOB\":6.9266,\r\n  \"BRL\":4.9123,\r\n  \"BSD\":1.0000,\r\n  \"BTN\":83.0580,\r\n  \"BWP\":13.6086,\r\n  \"BYN\":2.9807,\r\n  \"BZD\":2.0000,\r\n  \"CAD\":1.3516,\r\n  \"CDF\":2393.5315,\r\n  \"CHF\":0.8952,\r\n  \"CLP\":881.3606,\r\n  \"CNY\":7.2821,\r\n  \"COP\":3962.2013,\r\n  \"CRC\":533.9442,\r\n  \"CUP\":24.0000,\r\n  \"CVE\":103.3868,\r\n  \"CZK\":22.9217,\r\n  \"DJF\":177.7210,\r\n  \"DKK\":7.0008,\r\n  \"DOP\":56.8017,\r\n  \"DZD\":137.3231,\r\n  \"EGP\":30.9285,\r\n  \"ERN\":15.0000,\r\n  \"ETB\":55.4531,\r\n  \"EUR\":0.9377,\r\n  \"FJD\":2.2618,\r\n  \"FKP\":0.8048,\r\n  \"FOK\":6.9950,\r\n  \"GBP\":0.8051,\r\n  \"GEL\":2.6300,\r\n  \"GGP\":0.8048,\r\n  \"GHS\":11.4931,\r\n  \"GIP\":0.8048,\r\n  \"GMD\":64.5632,\r\n  \"GNF\":8585.1308,\r\n  \"GTQ\":7.8753,\r\n  \"GYD\":209.5359,\r\n  \"HKD\":7.8290,\r\n  \"HNL\":24.6525,\r\n  \"HRK\":7.0645,\r\n  \"HTG\":135.6143,\r\n  \"HUF\":359.5763,\r\n  \"IDR\":15371.2464,\r\n  \"ILS\":3.8197,\r\n  \"IMP\":0.8048,\r\n  \"INR\":83.0581,\r\n  \"IQD\":1312.7185,\r\n  \"IRR\":41994.5394,\r\n  \"ISK\":135.0588,\r\n  \"JEP\":0.8048,\r\n  \"JMD\":154.6390,\r\n  \"JOD\":0.7090,\r\n  \"JPY\":147.3477,\r\n  \"KES\":147.0317,\r\n  \"KGS\":88.2916,\r\n  \"KHR\":4117.4044,\r\n  \"KID\":1.5536,\r\n  \"KMF\":461.2793,\r\n  \"KRW\":1328.2571,\r\n  \"KWD\":0.3088,\r\n  \"KYD\":0.8333,\r\n  \"KZT\":465.3975,\r\n  \"LAK\":20117.2932,\r\n  \"LBP\":15000.0000,\r\n  \"LKR\":322.8189,\r\n  \"LRD\":189.9760,\r\n  \"LSL\":18.9935,\r\n  \"LYD\":4.8564,\r\n  \"MAD\":10.2194,\r\n  \"MDL\":17.9580,\r\n  \"MGA\":4501.6049,\r\n  \"MKD\":57.4190,\r\n  \"MMK\":2100.4366,\r\n  \"MNT\":3493.8787,\r\n  \"MOP\":8.0639,\r\n  \"MRU\":38.2078,\r\n  \"MUR\":44.7578,\r\n  \"MVR\":15.4570,\r\n  \"MWK\":1106.6777,\r\n  \"MXN\":17.1080,\r\n  \"MYR\":4.6816,\r\n  \"MZN\":63.9238,\r\n  \"NAD\":18.9935,\r\n  \"NGN\":752.9603,\r\n  \"NIO\":36.6178,\r\n  \"NOK\":10.7359,\r\n  \"NPR\":132.8928,\r\n  \"NZD\":1.6914,\r\n  \"OMR\":0.3845,\r\n  \"PAB\":1.0000,\r\n  \"PEN\":3.7077,\r\n  \"PGK\":3.6401,\r\n  \"PHP\":56.7181,\r\n  \"PKR\":298.3830,\r\n  \"PLN\":4.3341,\r\n  \"PYG\":7307.9128,\r\n  \"QAR\":3.6400,\r\n  \"RON\":4.6296,\r\n  \"RSD\":109.1852,\r\n  \"RUB\":96.0182,\r\n  \"RWF\":1222.2990,\r\n  \"SAR\":3.7500,\r\n  \"SBD\":8.5131,\r\n  \"SCR\":13.0114,\r\n  \"SDG\":542.2321,\r\n  \"SEK\":11.1714,\r\n  \"SGD\":1.3626,\r\n  \"SHP\":0.8048,\r\n  \"SLE\":22.2562,\r\n  \"SLL\":22256.1938,\r\n  \"SOS\":571.9576,\r\n  \"SRD\":38.1819,\r\n  \"SSP\":1011.2413,\r\n  \"STN\":22.9717,\r\n  \"SYP\":12956.3637,\r\n  \"SZL\":18.9935,\r\n  \"THB\":35.7633,\r\n  \"TJS\":10.9551,\r\n  \"TMT\":3.5012,\r\n  \"TND\":3.1373,\r\n  \"TOP\":2.3756,\r\n  \"TRY\":26.9552,\r\n  \"TTD\":6.7494,\r\n  \"TVD\":1.5536,\r\n  \"TWD\":31.9029,\r\n  \"TZS\":2508.1099,\r\n  \"UAH\":36.9253,\r\n  \"UGX\":3723.5727,\r\n  \"UYU\":38.1760,\r\n  \"UZS\":12224.6036,\r\n  \"VES\":33.5078,\r\n  \"VND\":24237.9655,\r\n  \"VUV\":121.6219,\r\n  \"WST\":2.7516,\r\n  \"XAF\":615.0391,\r\n  \"XCD\":2.7000,\r\n  \"XDR\":0.7560,\r\n  \"XOF\":615.0391,\r\n  \"XPF\":111.8882,\r\n  \"YER\":250.4758,\r\n  \"ZAR\":18.9956,\r\n  \"ZMW\":21.1169,\r\n  \"ZWL\":4841.8425\r\n }\r\n}";

        internal static IEnumerable<Currency> TestCurrencies = new[]
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

        internal static CzecBankModel StubCzecBankModel = new CzecBankModel()
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
    }
}
