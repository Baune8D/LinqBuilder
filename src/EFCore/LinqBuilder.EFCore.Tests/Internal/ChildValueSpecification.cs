using System;
using System.Linq;
using System.Linq.Expressions;
using LinqBuilder.EFCore.Testing;

namespace LinqBuilder.EFCore.Tests.Internal
{
    public class ChildValueSpecification : Specification<SomeEntity>
    {
        private readonly int _value;

        public ChildValueSpecification(int value)
        {
            _value = value;
        }

        public override Expression<Func<SomeEntity, bool>> AsExpression()
        {
            return entity => entity.ChildEntities.Any(x => x.Value == _value);
        }
    }
}
