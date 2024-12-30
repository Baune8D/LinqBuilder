using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LinqBuilder.Internal;
using LinqBuilder.OrderBy;
using LinqBuilder.Tests.Data;
using Xunit;

namespace LinqBuilder.Tests.Internal;

public class SpecificationBaseTests
{
    [Fact]
    public void Configuration_NewOrderSpecification_ShouldBeCorrectlyConfigured()
    {
        var configuration = new SpecificationBase<Entity>(new OrderSpecification<Entity, int>(x => x.Value1));
        configuration.QuerySpecification.Internal.Should().NotBeNull();
        configuration.QuerySpecification.Should().NotBeNull();
        configuration.QuerySpecification.AsExpression().Should().BeNull();
        configuration.QuerySpecification.AsFunc().Should().BeNull();
        configuration.OrderSpecifications.Count.Should().Be(1);
    }

    [Fact]
    public void Configuration_NewSpecification_ShouldBeCorrectlyConfigured()
    {
        var configuration = new SpecificationBase<Entity>(new Specification<Entity>());
        configuration.QuerySpecification.Internal.Should().NotBeNull();
        configuration.QuerySpecification.Should().NotBeNull();
        configuration.QuerySpecification.AsExpression().Should().BeNull();
        configuration.QuerySpecification.AsFunc().Should().BeNull();
        configuration.OrderSpecifications.Any().Should().BeFalse();
    }

    [Fact]
    public void Configuration_NewOrderSpecificationAndNewSpecification_ShouldBeCorrectlyConfigured()
    {
        var configuration = new SpecificationBase<Entity>(new Specification<Entity>(), new List<IOrderSpecification<Entity>>
        {
            new OrderSpecification<Entity, int>(x => x.Value1),
        });
        configuration.QuerySpecification.Internal.Should().NotBeNull();
        configuration.QuerySpecification.Should().NotBeNull();
        configuration.QuerySpecification.AsExpression().Should().BeNull();
        configuration.QuerySpecification.AsFunc().Should().BeNull();
        configuration.OrderSpecifications.Count.Should().Be(1);
    }
}
