using System;
using System.Linq.Expressions;

namespace LinqBuilder.Tests.Data.Specifications
{
    public class MultiEntitySpecification2 : MultiSpecification<Entity, Entity2>
    {
        public override Expression<Func<Entity, bool>> AsExpressionForEntity1()
        {
            return entity => entity.Value1 == 1;
        }

        public override Expression<Func<Entity2, bool>> AsExpressionForEntity2()
        {
            return entity => entity.Value2 == 2;
        }
    }
}
