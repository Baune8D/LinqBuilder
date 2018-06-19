using System;
using System.Threading.Tasks;
using LinqBuilder.EFCore.Tests.TestHelpers;
using LinqBuilder.OrderBy;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace LinqBuilder.EFCore.Tests
{
    public class IntegrationTests : IDisposable
    {
        private readonly DbFixture _dbFixture;

        public IntegrationTests()
        {
            _dbFixture = new DbFixture();
            _dbFixture.AddEntity(2, 1, 2);
            _dbFixture.AddEntity(1, 2, 3);
            _dbFixture.AddEntity(3, 1, 1);
            _dbFixture.Context.SaveChanges();
        }

        [Fact]
        public async Task ExeQueryAsync_ChildSpecification_ShouldReturnCorrectResult()
        {
            var specifiction = new ChildValueSpecification(1)
                .Or(new ChildValueSpecification(2));
            
            var result = await _dbFixture.Context.Entities
                .ExeQuery(specifiction)
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
