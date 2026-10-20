using System.Text.Json;
using FluentAssertions;

namespace ByteSizeTests;

using static JsonSerializer;

public class ByteSizeJsonSerializationTests() :
    BaseJsonConverterTests<ByteSize>(Gen.ByteSize, x => $"{x.TotalBytes}")
{
    [Fact] public void ShouldDeserialize() => DeserializeTest();
    [Fact] public void ShouldSerialize() => SerializeTest();
}

public abstract class BaseJsonConverterTests<T>(Func<T> getValue, Func<T, string>? getExpected = null)
{
    readonly Func<T, string> getExpected = getExpected ?? (x => x?.ToString() ?? "null");

    record TestType(T Data);

    public void DeserializeTest()
    {
        var value = getValue();
        var expected = getExpected(value);
        var result = Deserialize<TestType>($$"""{"Data":{{expected}}}""");
        result!.Data.Should().Be(value);
    }

    public void SerializeTest()
    {
        var value = getValue();
        var expected = getExpected(value);
        var json = Serialize(new TestType(value));
        var result = $$"""{"Data":{{expected}}}""";
        json.Should().Be(result);
    }
}
