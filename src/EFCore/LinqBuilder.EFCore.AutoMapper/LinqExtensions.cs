using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace LinqBuilder.EFCore.AutoMapper
{
    public static class LinqExtensions
    {
        public static IQueryable<T> ExeSpec<TEntity, T>(this IQueryable<TEntity> query, ISpecification<T> specification, IConfigurationProvider configuration, bool skipSort = false)
            where TEntity : class
            where T : class
        {
            return query.ProjectTo<T>(configuration).ExeSpec(specification, skipSort);
        }
    }
}
