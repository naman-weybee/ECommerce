using Newtonsoft.Json;

namespace ECommerce.Domain.ValueObjects.JsonConverters
{
    public class MoneyJsonConverter : JsonConverter<Money>
    {
        public override Money ReadJson(JsonReader reader, Type objectType, Money existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var amount = serializer.Deserialize<decimal>(reader);
            return new Money(amount);
        }

        public override void WriteJson(JsonWriter writer, Money value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Amount");
            serializer.Serialize(writer, value.Amount);
            writer.WriteEndObject();
        }
    }
}