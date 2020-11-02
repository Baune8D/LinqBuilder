using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderBy.Tests.Internal
{
    public class Specification : OrderSpecification<Entity, int>
    {
        public Specification(Sort sort = Sort.Ascending)
            : base(sort)
        {
        }

        public override Expression<Func<Entity, int>> AsExpression()
        {
            return entity => entity.Value1;
        }
    }
}
