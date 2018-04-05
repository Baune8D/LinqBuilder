using System;
using System.Threading.Tasks;
using LinqBuilder.EFCore.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.EFCore.Tests
{
    public class EntityFrameworkCoreTests : IDisposable
    {
        private readonly DbFixture _dbFixture;

        public EntityFrameworkCoreTests()
        {
            _dbFixture = new DbFixture();
            _dbFixture.AddEntity(2, 1); // Id 1
            _dbFixture.AddEntity(1, 1); // Id 2
            _dbFixture.AddEntity(1, 2); // Id 3
            _dbFixture.Context.SaveChanges();
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

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
    }
}
