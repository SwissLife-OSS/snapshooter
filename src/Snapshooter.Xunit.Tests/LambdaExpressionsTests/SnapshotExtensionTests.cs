using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions.Primitives;
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

    /// <summary>
    /// Converts a Lambda Expression into a JsonPath
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public static class LambdaPath<TSource>
    {
        public static string Get<TResult>(Expression<Func<TSource, TResult>> expression)
        {
            var visitor = new PathPrintVisitor();
            visitor.Visit(expression.Body);
            visitor.Path.Reverse();
            return string.Join("", visitor.Path)
                .Substring(1);
        }

        private class PathPrintVisitor : ExpressionVisitor
        {
            internal readonly List<string> Path = new();

            protected override Expression VisitMember(MemberExpression node)
            {
                if (!(node.Member is PropertyInfo or FieldInfo))
                {
                    throw new ArgumentException("The path can only contain properties or fields", nameof(node));
                }

                Path.Add($".{node.Member.Name}");
                return base.VisitMember(node);
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method.Name == "get_Item") // index access for IList
                {
                    var argument = node.Arguments.FirstOrDefault();
                    if (argument != null)
                    {
                        HandleIndexes(argument);
                    }
                }

                return base.VisitMethodCall(node);
            }

            private void HandleIndexes(Expression argument)
            {
                if (argument.NodeType == ExpressionType.Constant && argument.Type == typeof(int))
                {
                    int? value = (int)((ConstantExpression)argument).Value;
                    Path.Add(value is 42 or -1 or null ? "[*]" : $"[{value}]");
                }
            }

            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (node.NodeType == ExpressionType.ArrayIndex &&
                    node.Right.NodeType == ExpressionType.Constant)
                {
                    HandleIndexes(node.Right);
                }

                return base.VisitBinary(node);
            }
        }
    }
}
