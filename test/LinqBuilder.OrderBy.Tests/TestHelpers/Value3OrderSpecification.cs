using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderBy.Tests.TestHelpers
{
    public class Value3OrderSpecification : OrderSpecification<Entity, int>
    {
        public Value3OrderSpecification(Sort sort = Sort.Ascending) : base(sort) { }

        public override Expression<Func<Entity, int>> AsExpression()
        {
            return entity => entity.Value3;
        }
    }
}
