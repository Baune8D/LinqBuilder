using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderBy.Tests.TestHelpers
{
    public class Value2OrderSpecification : OrderSpecification<Entity, int>
    {
        public Value2OrderSpecification(Sort sort = Sort.Ascending) : base(sort) { }

        public override Expression<Func<Entity, int>> AsExpression()
        {
            return entity => entity.Value2;
        }
    }
}
