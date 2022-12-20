using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> first,
            bool condition,
            Expression<Func<T, bool>> second)
        {
            return condition ? first.AndAlso(second) : first;
        }

        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> first,
            bool condition,
            Expression<Func<T, bool>> second)
        {
            return condition ? first.OrElse(second) : first;
        }

        private static Expression<T> Compose<T>(
            this Expression<T> first,
            Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var visitor = new ParameterVisitor(first.Parameters);
            var expression = merge(visitor.Visit(first.Body)!, visitor.Visit(second.Body)!);
            return Expression.Lambda<T>(expression, first.Parameters);
        }

        private sealed class ParameterVisitor : ExpressionVisitor
        {
            private readonly ReadOnlyCollection<ParameterExpression> _parameters;

            public ParameterVisitor(ReadOnlyCollection<ParameterExpression> parameters)
                => _parameters = parameters;

            public override Expression? Visit(Expression? node)
                => base.Visit(node);

            protected override Expression VisitParameter(ParameterExpression parameter)
                => _parameters.FirstOrDefault(s => s.Type == parameter.Type) ?? parameter;
        }
    }
}