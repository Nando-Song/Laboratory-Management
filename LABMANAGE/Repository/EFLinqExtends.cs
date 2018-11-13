using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Repository
{
    public static class ExpressionExtends
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            //var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            //return Expression.Lambda<Func<T, bool>> (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
            var parameter = Expression.Parameter(typeof(T));
            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);

        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            //var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            //return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
            var parameter = Expression.Parameter(typeof(T));
            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }
        public static IOrderedEnumerable<TElement> SortBy<TElement, TValue>(this IQueryable<TElement> query, Dictionary<Func<TElement, TValue>, SortType> selector)
        {
            IOrderedEnumerable<TElement> result = null;
            if (selector == null || selector.Count <= 0)
                throw new ArgumentNullException("排序字段不能为空");
            int i = 0;
            foreach (var item in selector)
            {
                if (i > 0)
                {
                    if (item.Value == SortType.Asc)
                        result.ThenBy(item.Key);
                    else
                        result.ThenByDescending(item.Key);
                }
                else
                {
                    if (item.Value == SortType.Asc)
                        result = query.OrderBy(item.Key);
                    else
                        result = query.OrderByDescending(item.Key);
                }
                i++;
            }
            return result;
        }
        
    }
    public enum SortType
    {
        /// <summary>
        /// 升序
        /// </summary>
        Asc,
        /// <summary>
        /// 降序
        /// </summary>
        Desc
    }
    public class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
