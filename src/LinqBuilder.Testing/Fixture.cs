using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Testing
{
    public class Fixture
    {
        private readonly List<Entity> _store;

        public Fixture()
        {
            _store = new List<Entity>();
        }

        public IReadOnlyList<Entity> Store => _store;

        public IEnumerable<Entity> Collection => _store.AsEnumerable();

        public IQueryable<Entity> Query => _store.AsQueryable();

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

            _store.Add(entity);
        }
    }
}
