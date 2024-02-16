using System.Reflection;
using System.Linq.Expressions;

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

        if (startsWith is not null)
        {
            var constant = Expression.Constant(value);
            var property = Expression.Property(parameter, name);
            return Expression.Call(property, startsWith, constant);
        }

        return null;
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
