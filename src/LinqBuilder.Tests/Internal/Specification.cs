using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.Internal
{
    public class Specification : Specification<Entity>
    {
        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value1 == 1;
        }
    }
}
