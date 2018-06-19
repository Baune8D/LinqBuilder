using System;
using System.Threading.Tasks;
using LinqBuilder.Core;
using LinqBuilder.EF6.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.EF6.Tests
{
    public class EntityFrameworkTests : IDisposable
    {
        private readonly ISpecification<Entity> _value1ShouldBe1 = Spec<Entity>.New(entity => entity.Value1 == 1);
        private readonly ISpecification<Entity> _value1ShouldBe2 = Spec<Entity>.New(entity => entity.Value1 == 2);
        private readonly ISpecification<Entity> _value1ShouldBe4 = Spec<Entity>.New(entity => entity.Value1 == 4);
        private readonly ISpecification<Entity> _value2ShouldBe3 = Spec<Entity>.New(entity => entity.Value2 == 3);

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
            var result = await _dbFixture.Context.Entities
                .AnyAsync(_value1ShouldBe1);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task AnyAsync_Specification_ShouldBeFalse()
        {
            var result = await _dbFixture.Context.Entities
                .AnyAsync(_value1ShouldBe4);

            result.ShouldBeFalse();
        }

        [Fact]
        public async Task AllAsync_Specification_ShouldBeTrue()
        {
            var result = await _dbFixture.Context.Entities
                .AllAsync(_value2ShouldBe3);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task AllAsync_Specification_ShouldBeFalse()
        {
            var result = await _dbFixture.Context.Entities
                .AllAsync(_value1ShouldBe1);

            result.ShouldBeFalse();
        }

        [Fact]
        public async Task CountAsync_Specification_ShouldBeTrue()
        {
            var result = await _dbFixture.Context.Entities
                .CountAsync(_value1ShouldBe1);

            result.ShouldBe(2);
        }

        [Fact]
        public async Task FirstAsync_Specification_ShouldReturnCorrectResult()
        {
            var result = await _dbFixture.Context.Entities
                .FirstAsync(_value1ShouldBe1);

            result.ShouldBe(_dbFixture.Context.Entities.Find(2));
        }

        [Fact]
        public async Task FirstOrDefaultAsync_Specification_ShouldReturnCorrectResult()
        {
            var result = await _dbFixture.Context.Entities
                .FirstOrDefaultAsync(_value1ShouldBe1);

            result.ShouldBe(_dbFixture.Context.Entities.Find(2));
        }

        [Fact]
        public async Task SingleAsync_Specification_ShouldReturnCorrectResult()
        {
            var result = await _dbFixture.Context.Entities
                .SingleAsync(_value1ShouldBe2);

            result.ShouldBe(_dbFixture.Context.Entities.Find(1));
        }

        [Fact]
        public async Task SingleOrDefaultAsync_Specification_ShouldReturnCorrectResult()
        {
            var result = await _dbFixture.Context.Entities
                .SingleOrDefaultAsync(_value1ShouldBe2);

            result.ShouldBe(_dbFixture.Context.Entities.Find(1));
        }

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
    }
}
