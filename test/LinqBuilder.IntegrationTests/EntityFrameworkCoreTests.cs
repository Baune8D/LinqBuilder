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
                    Date = DateTimeOffset.Now.AddMinutes(20),
                    Value = 1
                });
                context.Add(new TestData
                {
                    Date = DateTimeOffset.Now.AddMinutes(15),
                    Value = 2
                });
                context.Add(new TestData
                {
                    Date = DateTimeOffset.Now,
                    Value = 1
                });
                context.Add(new TestData
                {
                    Date = DateTimeOffset.Now.AddMinutes(10),
                    Value = 3
                });
                context.Add(new TestData
                {
                    Date = DateTimeOffset.Now.AddMinutes(5),
                    Value = 1
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
                .OrderBy(new Value1OrderSpecification(Order.Descending))
                .Skip(1)
                .Take(2);

            List<TestData> result;

            using (var context = new TestDbContext(_options))
            {
                result = context.TestData.ExeSpec(specifiction).ToList();
            }

            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe(4);
            result[0].Value.ShouldBe(3);
            result[1].Id.ShouldBe(5);
            result[1].Value.ShouldBe(1);
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
