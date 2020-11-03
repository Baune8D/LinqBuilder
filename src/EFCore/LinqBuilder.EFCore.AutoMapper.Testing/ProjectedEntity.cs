using AutoMapper;
using LinqBuilder.EFCore.Testing;

namespace LinqBuilder.EFCore.AutoMapper.Testing
{
    [AutoMap(typeof(SomeEntity))]
    public class ProjectedEntity
    {
        public int Id { get; set; }

        public int Value1 { get; set; }

        public int Value2 { get; set; }
    }
}
