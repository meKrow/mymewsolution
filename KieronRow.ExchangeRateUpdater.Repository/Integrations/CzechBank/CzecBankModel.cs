namespace ExchangeRateUpdater.Repository.Integrations.CzechBank
{
    public class Rate
    {
        public string validFor { get; set; }
        public int order { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public int amount { get; set; }
        public string currencyCode { get; set; }
        public decimal rate { get; set; }
    }

    public class CzecBankModel
    {
        public List<Rate> rates { get; set; }
    }
}
