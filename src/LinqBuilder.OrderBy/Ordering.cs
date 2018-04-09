using System.Collections.Generic;

namespace LinqBuilder.OrderBy
{
    public class Ordering<TEntity>
    {
        public List<IOrderSpecification<TEntity>> OrderList { get; }
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public Ordering(List<IOrderSpecification<TEntity>> orderList, int? skip = null, int? take = null)
        {
            OrderList = orderList;
            Skip = skip;
            Take = take;
        }
    }
}
