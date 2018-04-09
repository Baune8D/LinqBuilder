using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderBy
{
    public class OrderedSpecification<TEntity> : IOrderedSpecification<TEntity>
        where TEntity : class
    {
        private readonly ISpecificationQuery<TEntity> _specification;
        private readonly Ordering<TEntity> _ordering;

        public OrderedSpecification(Ordering<TEntity> ordering)
        {
            _ordering = ordering;
        }

        public OrderedSpecification(ISpecificationQuery<TEntity> specificaiton, Ordering<TEntity> ordering) : this(ordering)
        {
            _specification = specificaiton;
        }

        public Ordering<TEntity> GetOrdering()
        {
            return _ordering;
        }

        public IOrderedSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> orderSpecification)
        {
            _ordering.OrderList.Add(orderSpecification);
            return new OrderedSpecification<TEntity>(_specification, _ordering);
        }

        public IOrderedSpecification<TEntity> Skip(int count)
        {
            _ordering.Skip = count;
            return this;
        }

        public IOrderedSpecification<TEntity> Take(int count)
        {
            _ordering.Take = count;
            return this;
        }

        public IOrderedSpecification<TEntity> Paginate(int pageNo, int pageSize)
        {
            Skip((pageNo - 1) * pageSize);
            Take(pageSize);
            return this;
        }

        public IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            if (_specification != null) query = _specification.Invoke(query);
            var orderedQuery = _ordering.OrderList[0].InvokeSort(query);

            for (var i = 1; i < _ordering.OrderList.Count; i++)
            {
                orderedQuery = _ordering.OrderList[i].InvokeSort(orderedQuery);
            }

            return SkipTake(orderedQuery);
        }

        public IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            if (_specification != null) collection = _specification.Invoke(collection);
            var orderedCollection = _ordering.OrderList[0].InvokeSort(collection);

            for (var i = 1; i < _ordering.OrderList.Count; i++)
            {
                orderedCollection = _ordering.OrderList[i].InvokeSort(orderedCollection);
            }

            return SkipTake(orderedCollection);
        }

        private IEnumerable<TEntity> SkipTake(IEnumerable<TEntity> collection)
        {
            if (_ordering.Skip.HasValue) collection = collection.Skip(_ordering.Skip.Value);
            if (_ordering.Take.HasValue) collection = collection.Take(_ordering.Take.Value);
            return collection;
        }

        private IQueryable<TEntity> SkipTake(IQueryable<TEntity> query)
        {
            if (_ordering.Skip.HasValue) query = query.Skip(_ordering.Skip.Value);
            if (_ordering.Take.HasValue) query = query.Take(_ordering.Take.Value);
            return query;
        }
    }
}
