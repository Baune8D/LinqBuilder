using System;
using System.Linq;
using System.Linq.Expressions;
using LinqBuilder.Specifications;

namespace LinqBuilder.IntegrationTests.TestHelpers
{
    public class ChildValue1Specification : Specification<Entity>
    {
        private readonly int _value;

        public ChildValue1Specification(int value)
        {
            _value = value;
        }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.ChildEntities.Any(x => x.Value1 == _value);
        }
    }
}
