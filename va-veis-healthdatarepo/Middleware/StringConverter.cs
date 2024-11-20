using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace va_veis_healthdatarepo.Middleware
{
    public class StringConverter : JsonConverter<string>
    {
        public StringConverter() { }

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            double strToDbl = 0.0;
            ulong strToLng = ulong.MinValue;
            int strToInt = 0;

            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetDouble(out strToDbl))
                {
                    return strToDbl.ToString();
                }
                if (reader.TryGetInt32(out strToInt))
                {
                    return strToInt.ToString();
                }
                if (reader.TryGetUInt64(out strToLng))
                {
                    return strToLng.ToString();
                }

            }
            if (reader.TokenType == JsonTokenType.String)
            {
                //ulong retValue = ulong.MinValue;
                string value = reader.GetString();
                if (value.Contains('+') && value.Length == 19)
                {
                    value = value.Substring(0, value.IndexOf('+'));

                    bool success = ulong.TryParse(value, out strToLng);
                    if (success)
                    {
                        return strToLng.ToString();
                    }
                }
                else
                    return reader.GetString();
            }
            if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
            {
                return reader.TokenType.ToString();
            }
            else
            {
                return reader.GetString();
            }

            throw new System.Text.Json.JsonException();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}