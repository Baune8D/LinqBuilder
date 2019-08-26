using System.Collections.Generic;
using LinqBuilder.EFCore.Tests.Shared;

namespace LinqBuilder.EFCore.AutoMapper.Tests.TestHelpers
{
    public class ProjectedEntity
    {
        public int Id { get; set; }

        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public ICollection<ChildEntity> ChildEntities { get; set; }
    }
}
