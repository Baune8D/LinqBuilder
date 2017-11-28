using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public class CompositeOrderSpecification<T> : ICompositeOrderSpecification<T>
    {
        private readonly ISpecification<T> _specification;
        private readonly List<IOrderSpecification<T>> _orderList;

        private int? _skip;
        private int? _take;

        public CompositeOrderSpecification(List<IOrderSpecification<T>> orderList, IOrderSpecification<T> right, int? skip = null, int? take = null)
        {
            _orderList = orderList;
            _orderList.Add(right);

            if (skip.HasValue) _skip = skip;
            if (take.HasValue) _take = take;
        }

        public CompositeOrderSpecification(ISpecification<T> specificaiton, List<IOrderSpecification<T>> orderList, IOrderSpecification<T> right, int? skip = null, int? take = null)
            : this(orderList, right, skip, take)
        {
            _specification = specificaiton;
        }

        public ICompositeOrderSpecification<T> ThenBy(IOrderSpecification<T> other)
        {
            return new CompositeOrderSpecification<T>(_specification, _orderList, other, _skip, _take);
        }

        public ICompositeOrderSpecification<T> Skip(int count)
        {
            _skip = count;
            return this;
        }

        public ICompositeOrderSpecification<T> Take(int count)
        {
            _take = count;
            return this;
        }

        public IQueryable<T> Invoke(IQueryable<T> query)
        { 
            if (_specification != null) query = _specification.Invoke(query);

            var orderedQuery = _orderList[0].InvokeOrdered(query);

            for (var i = 1; i < _orderList.Count; i++)
            {
                orderedQuery = _orderList[i].InvokeOrdered(orderedQuery);
            }

            return OrderHelper.SkipTake(orderedQuery.AsQueryable(), _skip, _take);
        }

        public IEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            if (_specification != null) collection = _specification.Invoke(collection);

            var orderedCollection = _orderList[0].InvokeOrdered(collection);

            for (var i = 1; i < _orderList.Count; i++)
            {
                orderedCollection = _orderList[i].InvokeOrdered(orderedCollection);
            }

            return OrderHelper.SkipTake(orderedCollection.AsEnumerable(), _skip, _take);
        }
    }
}
