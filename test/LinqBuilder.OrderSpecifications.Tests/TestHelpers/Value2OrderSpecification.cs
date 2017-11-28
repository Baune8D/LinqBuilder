using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications.Tests.TestHelpers
{
    public class Value2OrderSpecification : OrderSpecification<TestEntity, int>
    {
        public Value2OrderSpecification(Order direction = Order.Ascending) : base(direction) { }

        public override Expression<Func<TestEntity, int>> AsExpression()
        {
            return entity => entity.Value2;
        }
    }
}
