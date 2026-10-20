using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteUtils;

sealed class ByteSizeJsonConverter : JsonConverter<ByteSize>
{
    public override ByteSize Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Unsafe.BitCast<long, ByteSize>(reader.GetInt64());

    public override void Write(Utf8JsonWriter writer, ByteSize value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(Unsafe.As<ByteSize, long>(ref value));
}
