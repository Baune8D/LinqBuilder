using AutoMapper;
using LinqBuilder.EFCore.Tests.Shared;

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
