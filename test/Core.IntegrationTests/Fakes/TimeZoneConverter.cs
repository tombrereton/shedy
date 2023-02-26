using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shedy.Core.IntegrationTests.Fakes;

public class TimeZoneConverter : JsonConverter<TimeZoneInfo>
{
    public override TimeZoneInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeZoneInfo.FromSerializedString(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, TimeZoneInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToSerializedString());
    }
}