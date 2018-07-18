using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class QuerySpecificationTests
    {
        [Fact]
        public void AsFunc_NoExpression_ShouldBeNull()
        {
            new Specification<Entity>().AsFunc().ShouldBeNull();
        }
    }
}
