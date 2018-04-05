using System;
using System.Threading.Tasks;
using LinqBuilder.EntityFrameworkCore.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.EntityFrameworkCore.Tests
{
    public class EntityFrameworkCoreTests : IDisposable
    {
        private readonly DbFixture _dbFixture;

        public EntityFrameworkCoreTests()
        {
            _dbFixture = new DbFixture();
            Seed();
        }

        [Fact]
        public async Task AnyAsync_Specification_ShouldBeTrue()
        {
            var specifiction = new Value1Specification(1)
                .And(new Value2Specification(2));

            var result = await _dbFixture.Context.Entities
                .AnyAsync(specifiction);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task AnyAsync_Specification_ShouldBeFalse()
        {
            var specifiction = new Value1Specification(2)
                .And(new Value2Specification(2));

            var result = await _dbFixture.Context.Entities
                    .AnyAsync(specifiction);

            result.ShouldBeFalse();
        }

        [Fact]
        public async Task AnyAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.AnyAsync(null));
        }
        
        [Fact]
        public async Task AllAsync_Specification_ShouldBeTrue()
        {
            var specifiction = new Value1Specification(1)
                .Or(new Value1Specification(2));

            var result = await _dbFixture.Context.Entities
                .AllAsync(specifiction);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task AllAsync_Specification_ShouldBeFalse()
        {
            var specifiction = new Value1Specification(1)
                .Or(new Value1Specification(3));

            var result = await _dbFixture.Context.Entities
                .AllAsync(specifiction);

            result.ShouldBeFalse();
        }

        [Fact]
        public async Task AllAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.AllAsync(null));
        }

        private void Seed()
        {
            AddEntity(2, 1); // 1
            AddEntity(1, 1); // 2
            AddEntity(1, 2); // 3
            _dbFixture.Context.SaveChanges();
        }

        private void AddEntity(int value1, int value2)
        {
            _dbFixture.Context.Entities.Add(new Entity
            {
                Value1 = value1,
                Value2 = value2
            });
        }

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
    }
}
