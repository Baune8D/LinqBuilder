using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.TestHelpers
{
    public class MultiEntitySpecification3 : MultiSpecification<Entity, Entity2, Entity3>
    {
        public override Expression<Func<Entity, bool>> AsExpressionForEntity1()
        {
            return entity => entity.Value1 == 1;
        }

        public override Expression<Func<Entity2, bool>> AsExpressionForEntity2()
        {
            return entity => entity.Value2 == 2;
        }

        public override Expression<Func<Entity3, bool>> AsExpressionForEntity3()
        {
            return entity => entity.Value3 == 3;
        }
    }
}
