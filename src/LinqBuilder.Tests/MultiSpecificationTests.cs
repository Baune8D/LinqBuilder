using System;
using FluentAssertions;
using LinqBuilder.Tests.Data;
using LinqBuilder.Tests.Data.Specifications;
using Xunit;

namespace LinqBuilder.Tests;

public class MultiSpecificationTests
{
    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification2_Default()
    {
        new MultiEntitySpecification2()
            .IsSatisfiedBy(new Entity { Value1 = 1 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification2_Entity()
    {
        new MultiEntitySpecification2()
            .For<Entity>()
            .IsSatisfiedBy(new Entity { Value1 = 1 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification2_Entity2()
    {
        new MultiEntitySpecification2()
            .For<Entity2>()
            .IsSatisfiedBy(new Entity2 { Value2 = 2 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification2_ShouldThrowException()
    {
        Action act = () => new MultiEntitySpecification2()
            .For<Entity3>()
            .IsSatisfiedBy(new Entity3());

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification3_Default()
    {
        new MultiEntitySpecification3()
            .IsSatisfiedBy(new Entity { Value1 = 1 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification3_Entity()
    {
        new MultiEntitySpecification3()
            .For<Entity>()
            .IsSatisfiedBy(new Entity { Value1 = 1 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification3_Entity2()
    {
        new MultiEntitySpecification3()
            .For<Entity2>()
            .IsSatisfiedBy(new Entity2 { Value2 = 2 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification3_Entity3()
    {
        new MultiEntitySpecification3()
            .For<Entity3>()
            .IsSatisfiedBy(new Entity3 { Value3 = 3 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification3_ShouldThrowException()
    {
        Action act = () => new MultiEntitySpecification3()
            .For<Entity4>()
            .IsSatisfiedBy(new Entity4());

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification4_Default()
    {
        new MultiEntitySpecification4()
            .IsSatisfiedBy(new Entity { Value1 = 1 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification4_Entity()
    {
        new MultiEntitySpecification4()
            .For<Entity>()
            .IsSatisfiedBy(new Entity { Value1 = 1 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification4_Entity2()
    {
        new MultiEntitySpecification4()
            .For<Entity2>()
            .IsSatisfiedBy(new Entity2 { Value2 = 2 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification4_Entity3()
    {
        new MultiEntitySpecification4()
            .For<Entity3>()
            .IsSatisfiedBy(new Entity3 { Value3 = 3 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification4_Entity4()
    {
        new MultiEntitySpecification4()
            .For<Entity4>()
            .IsSatisfiedBy(new Entity4 { Value4 = 4 });
    }

    [Fact]
    public void IsSatisfiedBy_MultiEntitySpecification4_ShouldThrowException()
    {
        Action act = () => new MultiEntitySpecification4()
            .For<Entity5>()
            .IsSatisfiedBy(new Entity5());

        act.Should().Throw<Exception>();
    }
}
