using System.Runtime.CompilerServices;
using Bogus;

namespace Backdash.Tests;

public static class Initializer
{
    const int DefaultSeed = 42;

    [ModuleInitializer]
    public static void Init() => Randomizer.Seed = new(DefaultSeed);
}
