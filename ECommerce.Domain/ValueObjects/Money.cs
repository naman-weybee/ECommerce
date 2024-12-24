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

        public Money Multiply(int factor)
        {
            if (factor < 0)
                throw new ArgumentException("Factor cannot be negative.", nameof(factor));

            return new Money(Amount * factor);
        }

        public Money Add(Money other)
        {
            return new Money(Amount + other.Amount);
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

        public override bool Equals(object? obj)
        {
            return obj is Money money && Amount == money.Amount;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}