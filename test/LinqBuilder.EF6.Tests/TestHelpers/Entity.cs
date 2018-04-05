using System.Collections.Generic;

namespace LinqBuilder.EF6.Tests.TestHelpers
{
    public class Entity
    {
        public int Id { get; set; }

        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public ICollection<ChildEntity> ChildEntities { get;set; }
    }
}
