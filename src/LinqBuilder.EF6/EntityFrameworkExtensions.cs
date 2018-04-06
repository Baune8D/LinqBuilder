using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LinqBuilder.EF6
{
    public static class EntityFrameworkExtensions
    {
        public static async Task<bool> AnyAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return await query.AnyAsync(specification.AsExpression());
        }

        public static async Task<bool> AllAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return await query.AllAsync(specification.AsExpression());
        }

        public static async Task<int> CountAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return await query.CountAsync(specification.AsExpression());
        }

        public static async Task<TEntity> FirstAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return await query.FirstAsync(specification.AsExpression());
        }

        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return await query.FirstOrDefaultAsync(specification.AsExpression());
        }

        public static async Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return await query.SingleAsync(specification.AsExpression());
        }

        public static async Task<TEntity> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return await query.SingleOrDefaultAsync(specification.AsExpression());
        }
    }
}
