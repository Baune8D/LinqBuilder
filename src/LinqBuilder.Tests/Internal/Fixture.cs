using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Tests.Internal
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

        public void AddToCollection(int value1, int value2)
        {
            Store.Add(new Entity
            {
                Value1 = value1,
                Value2 = value2,
            });
        }
    }
}
