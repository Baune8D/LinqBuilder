using System;
using System.Linq.Expressions;

namespace LinqBuilder.EntityFrameworkCore.Tests.TestHelpers
{
    public class Value2Specification : Specification<Entity>
    {
        private readonly int _value;

        public Value2Specification(int value)
        {
            _value = value;
        }

        public override Expression<Func<Entity, bool>> AsExpression()
        {
            return entity => entity.Value2 == _value;
        }
    }
}
