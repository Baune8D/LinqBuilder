using System;
using System.Threading.Tasks;
using FluentAssertions;
using LinqBuilder.EFCore.Testing;
using LinqBuilder.EFCore.Testing.Specifications;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LinqBuilder.EFCore.Tests
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
                .Or(new ChildValueSpecification(2));

            var result = await _testDb.Context.Entities
                .ExeSpec(specification)
                .ToListAsync();

            result.Count.Should().Be(2);
            result[0].Id.Should().Be(1);
            result[1].Id.Should().Be(3);
        }
    }
}
