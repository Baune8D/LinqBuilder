using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.IntegrationTests.TestHelpers
{
    public class Fixture
    {
        public readonly DbContextOptions<TestDbContext> Options;

        public Fixture()
        {
            Options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            Seed();
        }

        private void Seed()
        {
            using (var context = new TestDbContext(Options))
            {
                context.Add(new Entity
                {
                    Value1 = 1,
                    Value2 = 1,
                    ChildEntities = new List<ChildEntity>
                    {
                        new ChildEntity
                        {
                            Value1 = 3
                        }
                    }
                });

                context.Add(new Entity
                {
                    Value1 = 1,
                    Value2 = 1,
                    ChildEntities = new List<ChildEntity>
                    {
                        new ChildEntity
                        {
                            Value1 = 5
                        }
                    }
                });

                context.Add(new Entity
                {
                    Value1 = 2,
                    Value2 = 1
                });

                context.Add(new Entity
                {
                    Value1 = 3,
                    Value2 = 1
                });

                context.Add(new Entity
                {
                    Value1 = 3,
                    Value2 = 2
                });

                context.SaveChanges();
            }
        }
    }
}
