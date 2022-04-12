using System;
using System.Linq.Expressions;
using LinqBuilder.OrderBy;

namespace LinqBuilder.EF6.Testing.Specifications
{
    public class OrderSpecification : OrderSpecification<SomeEntity, int>
    {
        public OrderSpecification(Sort sort = Sort.Ascending)
            : base(sort)
        {
        }

        public override Expression<Func<SomeEntity, int>> AsExpression()
        {
            return entity => entity.Value1;
        }
    }
}
