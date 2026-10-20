using System.Numerics;
using System.Runtime.CompilerServices;

namespace ByteUtils;

static class MathI
{
    /// <summary>
    ///     Divide two integers ceiling the result
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilDiv(int x, int y) => x is 0 ? 0 : 1 + ((x - 1) / y);

    /// <summary>
    ///    Return the next power of two number greater than <paramref name="number" />
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long NextPowerOfTwo(long number) =>
        number switch
        {
            <= 1 => 1L,
            > 1L << 62 => long.MaxValue,
            _ => 1L << (64 - BitOperations.LeadingZeroCount((ulong)(number - 1))),
        };
}
