namespace ECommerce.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; }

        public Money(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            Amount = amount;
        }

        public static bool operator <=(Money m1, decimal amount)
        {
            return m1.Amount <= amount;
        }

        public static bool operator >=(Money m1, decimal amount)
        {
            return m1.Amount >= amount;
        }

        public static bool operator <(Money m1, decimal amount)
        {
            return m1.Amount < amount;
        }

        public static bool operator >(Money m1, decimal amount)
        {
            return m1.Amount > amount;
        }
    }
}