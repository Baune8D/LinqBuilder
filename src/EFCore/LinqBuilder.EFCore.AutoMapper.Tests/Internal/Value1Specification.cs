using System;
using System.Linq.Expressions;

namespace LinqBuilder.EFCore.AutoMapper.Tests.Internal
{
    public class Value1Specification : Specification<ProjectedEntity>
    {
        private readonly int _value;

        public Value1Specification(int value)
        {
            _value = value;
        }

        public override Expression<Func<ProjectedEntity, bool>> AsExpression()
        {
            return entity => entity.Value1 == _value;
        }
    }
}
