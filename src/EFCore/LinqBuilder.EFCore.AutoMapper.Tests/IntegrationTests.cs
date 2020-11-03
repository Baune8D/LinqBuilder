using System;
using System.Threading.Tasks;
using AutoMapper;
using DelegateDecompiler.EntityFrameworkCore;
using LinqBuilder.EFCore.AutoMapper.Testing;
using LinqBuilder.EFCore.AutoMapper.Testing.Specifications;
using LinqBuilder.EFCore.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace LinqBuilder.EFCore.AutoMapper.Tests
{
    [Collection("AutoMapper collection")]
    public sealed class IntegrationTests : IClassFixture<AutoMapperFixture>, IDisposable
    {
        private readonly IConfigurationProvider _mapperConfig;
        private readonly TestDb _testDb;

        public IntegrationTests(AutoMapperFixture autoMapperFixture)
        {
            _mapperConfig = autoMapperFixture.MapperConfig;
            _testDb = new TestDb();
            _testDb.AddEntity(2, 1);
            _testDb.AddEntity(1, 2);
            _testDb.AddEntity(3, 1);
            _testDb.Context.SaveChanges();
        }

        public void Dispose()
        {
            _testDb.Dispose();
        }

        [Fact]
        public async Task ExeSpecAsync_ChildSpecification_ShouldReturnCorrectResult()
        {
            var specification = new Value1Specification(1)
                .Or(new Value1Specification(3));

            var result = await _testDb.Context.Entities
                .ExeSpec(specification, _mapperConfig)
                .DecompileAsync()
                .ToListAsync();

            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe(2);
            result[1].Id.ShouldBe(3);
        }
    }
}
