using System.Text.Json;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Models.Entities;
using Type = System.Type;

namespace va_veis_healthdatarepo.Middleware
{
    public class FlagConverter : JsonConverter<Flags>
    {
        public override Flags Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var flagContent = reader.GetString();

            if(flagContent.Contains("total") && flagContent.Contains("\"total\": 1"))
            {
                return JsonSerializer.Deserialize<FPDSFlag>(ref reader);
            }
            if (typeToConvert == typeof(List<FPDSFlag>)) {
                return JsonSerializer.Deserialize<List<FPDSFlag>>(ref reader);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
