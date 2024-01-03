using FluentAssertions.Primitives;

// Extension of Fluent assertion to not loose type information of asserted object
namespace FluentAssertions;

/// <summary>
/// Contains a number of methods to assert that an <see cref="object"/> is in the expected state.
/// </summary>
public class TypedAssertions<T> : ObjectAssertions<T, TypedAssertions<T>>
{
    internal TypedAssertions(T value) : base(value)
    {
    }
}

/// <summary>
/// Contains extension methods for custom assertions in unit tests.
/// </summary>
public static class AssertionExtensions
{
    /// <summary>
    /// Returns an <see cref="TypedAssertions{T}"/> object that can be used to assert the
    /// current <see cref="object"/>.
    /// </summary>
    public static TypedAssertions<TSubject> Should<TSubject>(this TSubject actualValue)
    {
        return new TypedAssertions<TSubject>(actualValue);
    }
}
