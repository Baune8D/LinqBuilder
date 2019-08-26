using AutoMapper;

namespace LinqBuilder.EFCore.AutoMapper.Tests.TestHelpers
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Entity, ProjectedEntity>();
        }
    }
}
