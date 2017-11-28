using System;
using System.Linq.Expressions;
using LinqBuilder.OrderSpecifications;

namespace LinqBuilder.IntegrationTests.TestHelpers
{
    public class Value1OrderSpecification : OrderSpecification<TestData, DateTimeOffset>
    {
        public Value1OrderSpecification(Order direction = Order.Ascending) 
            : base(direction) { }

        public override Expression<Func<TestData, DateTimeOffset>> AsExpression()
        {
            return entity => entity.Date;
        }
    }
}
