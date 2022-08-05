using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Snapshooter.Exceptions;

namespace Snapshooter.Helpers;

/// <summary>
/// Converts a Lambda Expression into a JsonPath
/// </summary>
public static class LambdaPath<TSource>
{
    /// <summary>
    /// Converts the given expression into a jsonPath
    /// </summary>
    public static string Get<TResult>(Expression<Func<TSource, TResult>> expression)
    {
        var visitor = new PathPrintVisitor();
        visitor.Visit(expression.Body);
        visitor.Path.Reverse();

        var joinedPath = string.Join("", visitor.Path);

        if (string.IsNullOrWhiteSpace(joinedPath))
        {
            throw new LambdaPathConversionException(
                "Given lambda expression could not be converted to path");
        }

        return joinedPath.StartsWith(".") ? joinedPath.Substring(1) : joinedPath;
    }

    private class PathPrintVisitor : ExpressionVisitor
    {
        internal readonly List<string> Path = new();

        protected override Expression VisitMember(MemberExpression node)
        {
            if (!(node.Member is PropertyInfo or FieldInfo))
            {
                throw new LambdaPathConversionException(
                    "The path can only contain properties or fields");
            }

            Path.Add($".{node.Member.Name}");
            return base.VisitMember(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "get_Item") // index access for IList
            {
                Expression argument = node.Arguments.FirstOrDefault();
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
                int? value = (int?)((ConstantExpression)argument).Value;
                // the char '*' will be cast to the integer 42 
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
