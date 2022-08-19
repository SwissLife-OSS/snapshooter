using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions.Primitives;
using Snapshooter.Helpers;
using Snapshooter.Tests.Data;
using Xunit;

// Extension of Fluent assertion to not loose type information of asserted object
namespace FluentAssertions.Primitives
{
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
}

namespace Snapshooter.Xunit.Tests.LambdaExpressionsTests
{
    public class SnapshotExtensionTests
    {
        [Fact]
        public void MatchSnapshot_ShouldFluentAssertions_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot(o => o
                .AcceptField(m => m.Firstname)
                .AcceptField(m => m.DateOfBirth)
                .IgnoreFields(m => m.Children['*'].Name));
        }
    }

    public static class Extensions
    {
        public static void MatchSnapshot<TSubject>(
            this TypedAssertions<TSubject> currentResult,
            Func<MatchOptions<TSubject>, MatchOptions<TSubject>>? matchOptions = null)
        {
            var cleanedObject = currentResult.RemoveUnwantedWrappers();

            Func<MatchOptions, MatchOptions>? chainedMatchOptions =
                matchOptions != null ? m => matchOptions(new MatchOptions<TSubject>(m)) : null;

            Snapshot.Match(
                cleanedObject,
                chainedMatchOptions);
        }
    }

    /// <summary>
    /// MatchOption which also knows the type of the object we assert against.
    /// </summary>
    public class MatchOptions<TModel> : MatchOptions
    {
        public MatchOptions(MatchOptions predecessor)
        {
            _matchOperators = predecessor.MatchOperators.ToList();
        }

        public MatchOptions<TModel> AcceptField<TU>(Expression<Func<TModel, TU>> fieldPath)
        {
            var path = LambdaPath<TModel>.Get(fieldPath);
            Accept<TU>(path);

            return this;
        }

        public MatchOptions<TModel> IgnoreFields<TU>(Expression<Func<TModel, TU>> fieldPaths)
        {
            var path = LambdaPath<TModel>.Get(fieldPaths);
            IgnoreFields<TU>(path);

            return this;
        }
    }
}
