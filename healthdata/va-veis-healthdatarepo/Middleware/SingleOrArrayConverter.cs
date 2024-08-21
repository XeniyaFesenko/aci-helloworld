using System.Text.Json;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Models.Entities;

namespace va_veis_healthdatarepo.Middleware
{
    public class SingleOrArrayConverter : JsonConverter<List<FPDSFlag>>
    {
        public override List<FPDSFlag> Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.StartArray:
                    var list = new List<FPDSFlag>();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                            break;
                        list.Add(JsonSerializer.Deserialize<FPDSFlag>(ref reader, options));
                    }
                    return list;
                default:
                    return new List<FPDSFlag> { JsonSerializer.Deserialize<FPDSFlag>(ref reader, options) };
            }
        }

        public override void Write(Utf8JsonWriter writer, List<FPDSFlag> objectToWrite, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, objectToWrite, objectToWrite.GetType(), options);
    }
}
