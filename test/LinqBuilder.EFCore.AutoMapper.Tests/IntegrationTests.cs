using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqBuilder.Core;
using LinqBuilder.EFCore.AutoMapper.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace LinqBuilder.EFCore.AutoMapper.Tests
{
    [Collection("AutoMapper collection")]
    public class IntegrationTests : IDisposable
    {
        private readonly IConfigurationProvider _mapperConfig;
        private readonly DbFixture _dbFixture;

        public IntegrationTests(AutoMapperFixture autoMapperFixture)
        {
            _mapperConfig = autoMapperFixture.MapperConfig;
            _dbFixture = new DbFixture();
            _dbFixture.AddEntity(2, 1, 2);
            _dbFixture.AddEntity(1, 2, 3);
            _dbFixture.AddEntity(3, 1, 1);
            _dbFixture.Context.SaveChanges();
        }

        [Fact]
        public async Task ExeSpecAsync_ChildSpecification_ShouldReturnCorrectResult()
        {
            var specification = new ChildValueSpecification(1)
                .Or(new ChildValueSpecification(2));

            var result = await _dbFixture.Context.Entities
                .ProjectTo<ProjectedEntity>(_mapperConfig)
                .ExeSpec(specification)
                .ToListAsync();

            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe(1);
            result[1].Id.ShouldBe(3);
        }

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
    }
}
