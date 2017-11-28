using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.IntegrationTests.TestHelpers
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) 
            : base(options) { }

        public virtual DbSet<TestData> TestData { get; set; }
    }
}
