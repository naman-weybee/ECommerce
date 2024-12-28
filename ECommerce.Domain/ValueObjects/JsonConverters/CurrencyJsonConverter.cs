using Newtonsoft.Json;

namespace ECommerce.Domain.ValueObjects.JsonConverters
{
    public class CurrencyJsonConverter : JsonConverter<Currency>
    {
        public override Currency ReadJson(JsonReader reader, Type objectType, Currency existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var code = serializer.Deserialize<string>(reader);
            return new Currency(code);
        }

        public override void WriteJson(JsonWriter writer, Currency value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Code");
            serializer.Serialize(writer, value.Code);
            writer.WriteEndObject();
        }
    }
}