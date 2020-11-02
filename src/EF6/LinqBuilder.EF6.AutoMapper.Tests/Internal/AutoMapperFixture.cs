using System.Reflection;
using AutoMapper;

namespace LinqBuilder.EF6.AutoMapper.Tests.Internal
{
    public class AutoMapperFixture
    {
        public AutoMapperFixture()
        {
            MapperConfig = new MapperConfiguration(cfg =>
                cfg.AddMaps(Assembly.GetExecutingAssembly()));
        }

        public IConfigurationProvider MapperConfig { get; }
    }
}
