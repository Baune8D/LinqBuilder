using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LinqBuilder.EF6.Tests.Data;
using Xunit;

namespace LinqBuilder.EF6.Tests;

public sealed class EntityFrameworkExtensionsTests : IDisposable
{
    private readonly ISpecification<SomeEntity> _emptySpecification = Spec<SomeEntity>.New();
    private readonly ISpecification<SomeEntity> _value1ShouldBe1 = Spec<SomeEntity>.New(entity => entity.Value1 == 1);
    private readonly ISpecification<SomeEntity> _value1ShouldBe2 = Spec<SomeEntity>.New(entity => entity.Value1 == 2);
    private readonly ISpecification<SomeEntity> _value1ShouldBe4 = Spec<SomeEntity>.New(entity => entity.Value1 == 4);
    private readonly ISpecification<SomeEntity> _value2ShouldBe3 = Spec<SomeEntity>.New(entity => entity.Value2 == 3);

    private readonly TestDb _testDb;

    public EntityFrameworkExtensionsTests()
    {
        _testDb = new TestDb();
        _testDb.AddEntity(2, 3);
        _testDb.AddEntity(1, 3);
        _testDb.AddEntity(1, 3);
        _testDb.Context.SaveChanges();
    }

    public void Dispose()
    {
        _testDb.Dispose();
    }

    [Fact]
    public async Task AnyAsync_Specification_ShouldBeTrue()
    {
        var result = await _testDb.Context.Entities
            .AnyAsync(_value1ShouldBe1);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AnyAsync_Specification_ShouldBeFalse()
    {
        var result = await _testDb.Context.Entities
            .AnyAsync(_value1ShouldBe4);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task AnyAsync_EmptySpecification_ShouldBeTrue()
    {
        var result = await _testDb.Context.Entities
            .AnyAsync(_emptySpecification);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AllAsync_Specification_ShouldBeTrue()
    {
        var result = await _testDb.Context.Entities
            .AllAsync(_value2ShouldBe3);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AllAsync_Specification_ShouldBeFalse()
    {
        var result = await _testDb.Context.Entities
            .AllAsync(_value1ShouldBe1);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task AllAsync_EmptySpecification_ShouldBeTrue()
    {
        var result = await _testDb.Context.Entities
            .AllAsync(_emptySpecification);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task CountAsync_Specification_ShouldBeTrue()
    {
        var result = await _testDb.Context.Entities
            .CountAsync(_value1ShouldBe1);

        result.Should().Be(2);
    }

    [Fact]
    public async Task CountAsync_EmptySpecification_ShouldBeEqualCount()
    {
        var result = await _testDb.Context.Entities
            .CountAsync(_emptySpecification);

        result.Should().Be(_testDb.Context.Entities.Count());
    }

    [Fact]
    public async Task FirstAsync_Specification_ShouldReturnCorrectResult()
    {
        var result = await _testDb.Context.Entities
            .FirstAsync(_value1ShouldBe1);

        result.Should().Be(await _testDb.Context.Entities.FindAsync(2));
    }

    [Fact]
    public async Task FirstAsync_EmptySpecification_ShouldReturnCorrectResult()
    {
        var result = await _testDb.Context.Entities
            .FirstAsync(_emptySpecification);

        result.Should().Be(await _testDb.Context.Entities.FindAsync(1));
    }

    [Fact]
    public async Task FirstOrDefaultAsync_Specification_ShouldReturnCorrectResult()
    {
        var result = await _testDb.Context.Entities
            .FirstOrDefaultAsync(_value1ShouldBe1);

        result.Should().Be(await _testDb.Context.Entities.FindAsync(2));
    }

    [Fact]
    public async Task FirstOrDefaultAsync_EmptySpecification_ShouldReturnCorrectResult()
    {
        var result = await _testDb.Context.Entities
            .FirstOrDefaultAsync(_emptySpecification);

        result.Should().Be(await _testDb.Context.Entities.FindAsync(1));
    }

    [Fact]
    public async Task SingleAsync_Specification_ShouldReturnCorrectResult()
    {
        var result = await _testDb.Context.Entities
            .SingleAsync(_value1ShouldBe2);

        result.Should().Be(await _testDb.Context.Entities.FindAsync(1));
    }

    [Fact]
    public async Task SingleAsync_EmptySpecification_ShouldReturnCorrectResult()
    {
        Func<Task> act = () => _testDb.Context.Entities.SingleAsync(_emptySpecification);

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task SingleOrDefaultAsync_Specification_ShouldReturnCorrectResult()
    {
        var result = await _testDb.Context.Entities
            .SingleOrDefaultAsync(_value1ShouldBe2);

        result.Should().Be(await _testDb.Context.Entities.FindAsync(1));
    }

    [Fact]
    public async Task SingleOrDefaultAsync_EmptySpecification_ShouldReturnCorrectResult()
    {
        Func<Task> act = () => _testDb.Context.Entities.SingleOrDefaultAsync(_emptySpecification);

        await act.Should().ThrowAsync<InvalidOperationException>();
    }
}
