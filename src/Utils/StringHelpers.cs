#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ByteUtils;

readonly ref struct SpanStringBuilder(
    in Span<char> destination,
    ref int writtenCount,
    IFormatProvider? formatProvider = null
)
{
    readonly Span<char> buffer = destination;
    readonly ref int offset = ref writtenCount;

    Span<char> CurrentBuffer => offset >= buffer.Length ? [] : buffer[offset..];
    Span<char> WrittenSpan => buffer[..offset];

    public override string ToString() => new(WrittenSpan);

    public bool Write(scoped ReadOnlySpan<char> value)
    {
        if (!value.TryCopyTo(CurrentBuffer))
            return false;

        offset += value.Length;
        return true;
    }

    public bool Write<T>(in T value, ReadOnlySpan<char> format) where T : ISpanFormattable
    {
        if (!value.TryFormat(CurrentBuffer, out int written, format, formatProvider))
            return false;

        offset += written;
        return true;
    }

    public bool Write<T>(in T value) where T : ISpanFormattable => Write(in value, []);

#pragma warning restore S2325, CA1822

    [InterpolatedStringHandler]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal ref struct InterpolatedStringHandler
    {
        readonly SpanStringBuilder builder;
        bool succeeded = true;

        public InterpolatedStringHandler(int literalLength, int formattedCount, SpanStringBuilder builder) =>
            this.builder = builder;

        public void AppendLiteral(string value)
        {
            if (succeeded)
                succeeded = succeeded && builder.Write(value);
        }

        public void AppendLiteral(ReadOnlySpan<char> value)
        {
            if (succeeded)
                succeeded = succeeded && builder.Write(value);
        }

        public void AppendFormatted<T>(T value) where T : ISpanFormattable
        {
            if (succeeded)
                succeeded = succeeded && builder.Write(value);
        }

        public void AppendFormatted<T>(T value, string? format) where T : ISpanFormattable
        {
            if (succeeded)
                succeeded = succeeded && builder.Write(value, format);
        }
    }
}

readonly ref struct Utf8StringBuilder
{
    readonly Span<byte> buffer;
    readonly ref int offset;

    public Utf8StringBuilder(in Span<byte> bufferArg, ref int offset)
    {
        buffer = bufferArg;
        this.offset = ref offset;
    }

    Span<byte> CurrentBuffer => offset >= buffer.Length ? [] : buffer[offset..];

    public bool Write(ReadOnlySpan<char> value)
    {
        var dest = CurrentBuffer;
        if (dest.IsEmpty) return false;
        var size = System.Text.Encoding.UTF8.GetByteCount(value);
        var chars = size <= dest.Length
            ? value
            : value[..dest.Length];
        offset += System.Text.Encoding.UTF8.GetBytes(chars, dest);
        return true;
    }

    public bool Write(ReadOnlySpan<byte> value)
    {
        var dest = CurrentBuffer;
        if (dest.IsEmpty) return false;
        var bytes = value.Length <= dest.Length
            ? value
            : value[..dest.Length];
        bytes.CopyTo(dest);
        offset += value.Length;
        return true;
    }

    public bool Write<T>(in T value, ReadOnlySpan<char> format, IFormatProvider? provider = null)
        where T : IUtf8SpanFormattable
    {
        var dest = CurrentBuffer;
        if (dest.IsEmpty) return false;
        if (!value.TryFormat(dest, out var written, format, provider))
            return false;
        offset += written;
        return true;
    }

    public bool Write<T>(in T value) where T : IUtf8SpanFormattable => Write(in value, []);
}
