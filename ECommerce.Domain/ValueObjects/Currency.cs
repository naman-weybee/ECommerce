namespace ECommerce.Domain.ValueObjects
{
    public class Currency
    {
        public string Code { get; }

        public Currency(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
                throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(code));

            Code = code.ToUpper();
        }
    }
}