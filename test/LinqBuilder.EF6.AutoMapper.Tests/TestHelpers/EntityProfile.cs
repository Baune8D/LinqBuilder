using AutoMapper;
using LinqBuilder.EF6.Tests.Shared;

namespace LinqBuilder.EF6.AutoMapper.Tests.TestHelpers
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Entity, ProjectedEntity>();
        }
    }
}
