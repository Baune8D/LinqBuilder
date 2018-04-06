using System;
using System.Threading.Tasks;
using LinqBuilder.EF6.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.EF6.Tests
{
    public class EntityFrameworkTests : IDisposable
    {
        private readonly DbFixture _dbFixture;

        public EntityFrameworkTests()
        {
            _dbFixture = new DbFixture();
            _dbFixture.AddEntity(2, 3);
            _dbFixture.AddEntity(1, 3);
            _dbFixture.AddEntity(1, 3);
            _dbFixture.Context.SaveChanges();
        }

        [Fact]
        public async Task AnyAsync_Specification_ShouldBeTrue()
        {
            var specifiction = new Value1Specification(1);

            var result = await _dbFixture.Context.Entities
                .AnyAsync(specifiction);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task AnyAsync_Specification_ShouldBeFalse()
        {
            var specifiction = new Value1Specification(4);

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
            var specifiction = new Value2Specification(3);

            var result = await _dbFixture.Context.Entities
                .AllAsync(specifiction);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task AllAsync_Specification_ShouldBeFalse()
        {
            var specifiction = new Value1Specification(1);

            var result = await _dbFixture.Context.Entities
                .AllAsync(specifiction);

            result.ShouldBeFalse();
        }

        [Fact]
        public async Task AllAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.AllAsync(null));
        }

        [Fact]
        public async Task CountAsync_Specification_ShouldBeTrue()
        {
            var specifiction = new Value1Specification(1);

            var result = await _dbFixture.Context.Entities
                .CountAsync(specifiction);

            result.ShouldBe(2);
        }

        [Fact]
        public async Task CountAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.CountAsync(null));
        }

        [Fact]
        public async Task FirstAsync_Specification_ShouldReturnDbEntity()
        {
            var specifiction = new Value1Specification(1);

            var result = await _dbFixture.Context.Entities
                .FirstAsync(specifiction);

            result.ShouldBe(_dbFixture.Context.Entities.Find(2));
        }

        [Fact]
        public async Task FirstAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.FirstAsync(null));
        }

        [Fact]
        public async Task FirstOrDefaultAsync_Specification_ShouldReturnDbEntity()
        {
            var specifiction = new Value1Specification(1);

            var result = await _dbFixture.Context.Entities
                .FirstOrDefaultAsync(specifiction);

            result.ShouldBe(_dbFixture.Context.Entities.Find(2));
        }

        [Fact]
        public async Task FirstOrDefaultAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.FirstOrDefaultAsync(null));
        }

        [Fact]
        public async Task SingleAsync_Specification_ShouldReturnDbEntity()
        {
            var specifiction = new Value1Specification(2);

            var result = await _dbFixture.Context.Entities
                .SingleAsync(specifiction);

            result.ShouldBe(_dbFixture.Context.Entities.Find(1));
        }

        [Fact]
        public async Task SingleAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.SingleAsync(null));
        }

        [Fact]
        public async Task SingleOrDefaultAsync_Specification_ShouldReturnDbEntity()
        {
            var specifiction = new Value1Specification(2);

            var result = await _dbFixture.Context.Entities
                .SingleOrDefaultAsync(specifiction);

            result.ShouldBe(_dbFixture.Context.Entities.Find(1));
        }

        [Fact]
        public async Task SingleOrDefaultAsync_Null_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(_dbFixture.Context.Entities.SingleOrDefaultAsync(null));
        }

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
    }
}
