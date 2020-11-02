using System.Collections.Generic;
using AutoMapper;
using LinqBuilder.EF6.Testing;

namespace LinqBuilder.EF6.AutoMapper.Tests.Internal
{
    [AutoMap(typeof(SomeEntity))]
    public class ProjectedEntity
    {
        public int Id { get; set; }

        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public List<SomeChildEntity> ChildEntities { get; set; } = new List<SomeChildEntity>();
    }
}
