using Bogus;

namespace ByteSizeTests;

static class Gen
{
    public static readonly Faker Faker = new();
    public static Randomizer Random => Faker.Random;
    public static ByteSize ByteSize() => new(Faker.Random.Long(0));
}
