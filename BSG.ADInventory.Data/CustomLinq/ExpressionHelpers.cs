namespace BSG.ADInventory.Data.CustomLinq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using LinqKit;

    public static class ExpressionHelpers
    {
        /// <summary>
        /// Ands the expressions.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> AndExpressions<TEntity>(this Expression<Func<TEntity, bool>> left, Expression<Func<TEntity, bool>> right) where TEntity : class
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(
                new SwapingVisitor(left.Parameters[0], right.Parameters[0]).Visit(left.Body), right.Body), right.Parameters);
        }
    }
}
