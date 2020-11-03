using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Testing
{
    public class Fixture
    {
        public Fixture()
        {
            Store = new List<Entity>();
        }

        public List<Entity> Store { get; }

        public IEnumerable<Entity> Collection => Store.AsEnumerable();

        public IQueryable<Entity> Query => Store.AsQueryable();

        public void AddToCollection(int value1, int value2, int? value3 = null, int? value4 = null)
        {
            var entity = new Entity
            {
                Value1 = value1,
                Value2 = value2,
            };

            if (value3.HasValue)
            {
                entity.Value3 = value3.Value;
            }

            if (value4.HasValue)
            {
                entity.Value4 = value4.Value;
            }

            Store.Add(entity);
        }
    }
}
