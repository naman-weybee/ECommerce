using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Domain.ValueObjects.ValueConverters
{
    public class CurrencyConverter : ValueConverter<Currency, string>
    {
        public CurrencyConverter() : base(
            v => v.Code,
            v => new Currency(v))
        { }
    }
}