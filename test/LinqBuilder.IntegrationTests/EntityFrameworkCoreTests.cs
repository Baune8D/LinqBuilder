using System;
using System.Collections.Generic;
using System.Linq;
using LinqBuilder.IntegrationTests.TestHelpers;
using LinqBuilder.OrderSpecifications;
using LinqBuilder.Specifications;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace LinqBuilder.IntegrationTests
{
    public class EntityFrameworkCoreTests : IDisposable
    {
        private readonly DbContextOptions<TestDbContext> _options;

        public EntityFrameworkCoreTests()
        {
            _options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var context = new TestDbContext(_options))
            {
                context.Add(new TestData
                {
                    Value1 = 1
                });
                context.Add(new TestData
                {
                    Value1 = 2
                });
                context.Add(new TestData
                {
                    Value1 = 1,
                    Value2 = 2
                });
                context.Add(new TestData
                {
                    Value1 = 3
                });
                context.Add(new TestData
                {
                    Value1 = 1,
                    Value2 = 1
                });
                context.Add(new TestData
                {
                    Value1 = 4
                });
                
                context.SaveChanges();
            }
        }

        [Fact]
        public void ExeSpec_ComplexSpecification_ShouldReturnCorrectList()
        {
            var specifiction = new Specification<TestData>()
                .And(new Value1Specification(1))
                .Or(new Value1Specification(3))
                .OrderBy(new Value1OrderSpecification())
                .ThenBy(new Value2OrderSpecification(Order.Descending))
                .Skip(1)
                .Take(3);

            List<TestData> result;
            using (var context = new TestDbContext(_options))
            {
                result = context.TestData.ExeSpec(specifiction).ToList();
            }

            result.Count.ShouldBe(3);
            result[0].Id.ShouldBe(4);
            result[0].Value1.ShouldBe(3);
            result[1].Id.ShouldBe(5);
            result[1].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(1);
            result[2].Id.ShouldBe(5);
            result[2].Value1.ShouldBe(1);
            result[2].Value1.ShouldBe(1);
        }

        public void Dispose()
        {
            using (var context = new TestDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
