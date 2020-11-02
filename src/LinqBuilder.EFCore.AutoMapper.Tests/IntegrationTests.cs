using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqBuilder.Core;
using LinqBuilder.EFCore.AutoMapper.Tests.TestHelpers;
using LinqBuilder.EFCore.Tests.Shared;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace LinqBuilder.EFCore.AutoMapper.Tests
{
    [Collection("AutoMapper collection")]
    public class IntegrationTests : IClassFixture<AutoMapperFixture>, IDisposable
    {
        private readonly IConfigurationProvider _mapperConfig;
        private readonly TestDb _testDb;

        public IntegrationTests(AutoMapperFixture autoMapperFixture)
        {
            _mapperConfig = autoMapperFixture.MapperConfig;
            _testDb = new TestDb();
            _testDb.AddEntity(2, 1, 2);
            _testDb.AddEntity(1, 2, 3);
            _testDb.AddEntity(3, 1, 1);
            _testDb.Context.SaveChanges();
        }

        [Fact]
        public async Task ExeSpecAsync_ChildSpecification_ShouldReturnCorrectResult()
        {
            var specification = new ChildValueSpecification(1)
                .Or(new ChildValueSpecification(2));

            var result = await _testDb.Context.Entities
                .ProjectTo<ProjectedEntity>(_mapperConfig)
                .ExeSpec(specification)
                .ToListAsync();

            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe(1);
            result[1].Id.ShouldBe(3);
        }

        public void Dispose()
        {
            _testDb.Dispose();
        }
    }
}
