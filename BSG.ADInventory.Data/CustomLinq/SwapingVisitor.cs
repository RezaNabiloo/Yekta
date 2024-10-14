namespace BSG.ADInventory.Data.CustomLinq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public class SwapingVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public SwapingVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}
