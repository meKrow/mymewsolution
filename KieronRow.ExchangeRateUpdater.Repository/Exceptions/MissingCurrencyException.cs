namespace ExchangeRateUpdater.Repository.Exceptions
{
    public class MissingCurrencies : Exception
    {
        public MissingCurrencies(string message) : base(message) { }

        public MissingCurrencies()
            : base("Please supply at least one currency") { }
    }
}
