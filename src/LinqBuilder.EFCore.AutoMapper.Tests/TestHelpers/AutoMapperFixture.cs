using AutoMapper;

namespace LinqBuilder.EFCore.AutoMapper.Tests.TestHelpers
{
    public class AutoMapperFixture
    {
        public AutoMapperFixture()
        {
            MapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfile<EntityProfile>());
        }

        public IConfigurationProvider MapperConfig { get; }
    }
}
