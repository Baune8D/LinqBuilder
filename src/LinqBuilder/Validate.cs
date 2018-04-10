using System;

namespace LinqBuilder
{
    public static class Validate
    {
        public static void Specification<TEntity>(ISpecificationQuery<TEntity> specification, string paramName)
        {
            if (specification == null) throw new ArgumentNullException(paramName, "Cannot be null!");
        }
    }
}
