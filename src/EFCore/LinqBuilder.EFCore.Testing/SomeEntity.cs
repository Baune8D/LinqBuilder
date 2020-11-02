using System.Collections.Generic;

namespace LinqBuilder.EFCore.Testing
{
    public class SomeEntity
    {
        public int Id { get; set; }

        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public virtual ICollection<SomeChildEntity> ChildEntities { get; set; } = new HashSet<SomeChildEntity>();
    }
}
