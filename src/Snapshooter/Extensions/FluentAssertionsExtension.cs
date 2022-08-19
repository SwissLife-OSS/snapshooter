using FluentAssertions.Primitives;

// Extension of Fluent assertion to not loose type information of asserted object
namespace FluentAssertions;

public class TypedAssertions<T> : ObjectAssertions<T, TypedAssertions<T>>
{
    public TypedAssertions(T value) : base(value)
    {
    }
}

public static class Extensions
{
    public static TypedAssertions<TSubject> Should<TSubject>(this TSubject actualValue)
    {
        return new TypedAssertions<TSubject>(actualValue);
    }
}
