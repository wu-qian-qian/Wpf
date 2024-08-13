using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, T1>> ConnectEx<T, T1>(this Expression<Func<T, T1>> ex1, Expression<Func<T, T1>> ex2, ExpressionType type) where T1 : class
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            OperationVisitor visitor = new OperationVisitor(parameter);
            var left = visitor.Replace(ex1.Body);
            var right = visitor.Replace(ex1.Body);
            BinaryExpression body = default;
            switch (type)
            {
                case ExpressionType.And:
                    body = Expression.And(left, right);
                    break;
                case ExpressionType.AndAlso:
                    body = Expression.AndAlso(left, right);
                    break;
                case ExpressionType.Equal:
                    body = Expression.Equal(left, right);
                    break;
                case ExpressionType.OrElse:
                    body = Expression.OrElse(left, right);
                    break;
            }
            return Expression.Lambda<Func<T, T1>>(body, parameter);
        }
    }
}
