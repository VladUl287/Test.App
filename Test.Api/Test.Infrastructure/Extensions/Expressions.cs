using System.Reflection;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Test.Infrastructure.Extensions;

internal static class Expressions
{
    internal static Expression? StartsWith(this Expression parameter, string? value, string name)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        var startsWith = typeof(string).GetMethod(
            nameof(string.StartsWith), BindingFlags.Instance | BindingFlags.Public, new Type[] { typeof(string) });

        if (startsWith is null)
        {
            return null;
        }

        var constant = Expression.Constant(value);
        var property = Expression.Property(parameter, name);

        return Expression.Call(property, startsWith, constant);
    }

    internal static Expression? AndStartsWith(this Expression? previous, Expression parameter, string? value, string name)
    {
        var result = parameter.StartsWith(value, name);

        if (result is null)
        {
            return previous;
        }

        if (previous is null)
        {
            return result;
        }

        return Expression.And(previous, result);
    }

    public static Expression? AndEqual<T>(this Expression? previous, Expression parameter, T? value, string name)
    {
        if (value is null)
        {
            return previous;
        }

        var constant = Expression.Constant(value);
        var property = Expression.Property(parameter, name);
        var expression = Expression.Equal(property, constant);

        if (previous is null)
        {
            return expression;
        }

        return Expression.And(previous, expression);
    }
}
