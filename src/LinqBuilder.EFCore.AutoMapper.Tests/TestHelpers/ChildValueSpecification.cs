using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.EFCore.AutoMapper.Tests.TestHelpers
{
    public class ChildValueSpecification : Specification<ProjectedEntity>
    {
        private readonly int _value;

        public ChildValueSpecification(int value)
        {
            _value = value;
        }

        public override Expression<Func<ProjectedEntity, bool>> AsExpression()
        {
            return entity => entity.ChildEntities.Any(x => x.Value == _value);
        }
    }
}
