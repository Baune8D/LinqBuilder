using System;
using System.Data.Entity;
using System.Threading.Tasks;
using FluentAssertions;
using LinqBuilder.EF6.Tests.Data;
using LinqBuilder.EF6.Tests.Data.Specifications;
using LinqBuilder.OrderBy;
using Xunit;

namespace LinqBuilder.EF6.Tests
{
    public sealed class IntegrationTests : IDisposable
    {
        private readonly TestDb _testDb;

        public IntegrationTests()
        {
            _testDb = new TestDb();
            _testDb.AddEntity(2, 1, 2);
            _testDb.AddEntity(1, 2, 3);
            _testDb.AddEntity(3, 1, 1);
            _testDb.Context.SaveChanges();
        }

        public void Dispose()
        {
            _testDb.Dispose();
        }

        [Fact]
        public async Task ExeSpecAsync_ChildSpecification_ShouldReturnCorrectResult()
        {
            var specification = new ChildValueSpecification(1)
                .Or(new ChildValueSpecification(2))
                .OrderBy(new OrderSpecification());

            var result = await _testDb.Context.Entities
                .ExeSpec(specification)
                .ToListAsync();

            result.Count.Should().Be(2);
            result[0].Id.Should().Be(1);
            result[1].Id.Should().Be(3);
        }
    }
}
