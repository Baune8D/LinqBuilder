using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications.Tests.TestHelpers
{
    public class Value3OrderSpecification : OrderBySpecification<TestEntity>
    {
        public Value3OrderSpecification(Order direction = Order.Ascending) : base(direction) { }

        public override Expression<Func<TestEntity, IComparable>> AsExpression()
        {
            return entity => entity.Value3;
        }
    }
}
