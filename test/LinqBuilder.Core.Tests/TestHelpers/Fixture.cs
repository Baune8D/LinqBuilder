using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Core.Tests.TestHelpers
{
    public class Fixture
    {
        private readonly List<Entity> _collection;

        public Fixture()
        {
            _collection = new List<Entity>();
        }

        public IEnumerable<Entity> Collection => _collection.AsEnumerable();

        public IQueryable<Entity> Query => _collection.AsQueryable();

        public void AddToCollection(int value1, int value2, int value3)
        {
            _collection.Add(new Entity
            {
                Value1 = value1,
                Value2 = value2
            });
        }
    }
}
