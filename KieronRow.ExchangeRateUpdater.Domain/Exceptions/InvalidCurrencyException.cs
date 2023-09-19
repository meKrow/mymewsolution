namespace ExchangeRateUpdater.Repository.Exceptions
{
    public class InvalidCurrencyException : Exception
    {
        public InvalidCurrencyException(string message) : base(message) { }

        public InvalidCurrencyException()
            : base("At least one currency is invalid") { }
    }
}
