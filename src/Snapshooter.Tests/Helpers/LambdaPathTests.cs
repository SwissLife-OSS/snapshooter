using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Snapshooter.Helpers;
using Xunit;
using System.Linq;
using FluentAssertions;
using Snapshooter.Exceptions;
using TupleList = System.Collections.Generic.List<System.Tuple<string, System.Linq.Expressions.Expression<System.Func<Snapshooter.Tests.Helpers.Model, object>>>>;


namespace Snapshooter.Tests.Helpers
{
    public class LambdaPathTests
    {
        public static IEnumerable<object[]> Data() =>
            new TupleList
            {
                new(
                    "FooBar",
                    m => m.FooBar),
                new(
                    "Foo.Id",
                    m => m.Foo.Id),
                new(
                    "NestedType.MoreNesting.EvenMore.Field",
                    m => m.NestedType.MoreNesting.EvenMore.Field),
                new(
                    "Bars[0].Name",
                    m => m.Bars[0].Name),
                new(
                    "NestedType.MoreNestingList[0].EvenMoreArray[0].Field",
                    m => m.NestedType.MoreNestingList[0].EvenMoreArray[0].Field),
                new(
                    "Bars[*].Name",
                    m => m.Bars['*'].Name),
                new(
                    "NestedType.MoreNestingList[*].EvenMoreArray[*].Field",
                    m => m.NestedType.MoreNestingList[-1].EvenMoreArray['*'].Field),
                new(
                    "WithoutGetSet.MoreNesting.EvenMore.Field",
                    m => m.WithoutGetSet.MoreNesting.EvenMore.Field),
            }.Select(t => new object[] { t.Item2, t.Item1 });


        [Theory]
        [MemberData(nameof(Data))]
        public void LambdaPath_ValidExpression_CorrectJsonPath(
            Expression<Func<Model, object>> expression,
            string expected)
        {
            Assert.Equal(expected, LambdaPath<Model>.Get(expression));
        }

        [Fact]
        public void LambdaPath_InvalidExpression_Exception()
        {
            // Act
            Action action = () => LambdaPath<Model>.Get(m => m.IAmAFunction());

            // Assert
            action.Should().Throw<LambdaPathConversionException>();
        }
    }

    public class Model
    {
        public Foo Foo { get; set; }
        public IList<Bar> Bars { get; set; }

        public string FooBar { get; set; }

        public NestedType NestedType { get; set; }

        public NestedType WithoutGetSet;

        public bool IAmAFunction()
        {
            return false;
        }
    }

    public class NestedType
    {
        public MoreNesting MoreNesting { get; set; }
        public List<MoreNesting> MoreNestingList { get; set; }
    }

    public class MoreNesting
    {
        public EvenMore EvenMore { get; set; }
        public EvenMore[] EvenMoreArray { get; set; }
    }

    public class EvenMore
    {
        public Guid Field { get; set; }
    }

    public class Bar
    {
        public string Name { get; set; }
    }

    public class Foo
    {
        public int Id { get; set; }
    }
}
