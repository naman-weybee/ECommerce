using ECommerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Domain.ValueConverters
{
    public class MoneyConverter : ValueConverter<Money, decimal>
    {
        public MoneyConverter() : base(
            v => v.Amount,
            v => new Money(v))
        { }
    }
}