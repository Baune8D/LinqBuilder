using AutoMapper;

namespace LinqBuilder.EF6.AutoMapper.Tests.TestHelpers
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
