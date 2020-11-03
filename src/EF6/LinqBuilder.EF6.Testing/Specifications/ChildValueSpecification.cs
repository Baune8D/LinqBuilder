using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.EF6.Testing.Specifications
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
