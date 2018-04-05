using System;
using System.Data.SQLite;

namespace LinqBuilder.EntityFramework.Tests.TestHelpers
{
    public class DbFixture : IDisposable
    {
        public TestDbContext Context { get; private set; }

        public DbFixture()
        {
            Context = CreateContext();
        }

        // Tear down in-memory database.
        public void Dispose()
        {
            Context?.Dispose();
            Context = null;
        }

        private static TestDbContext CreateContext()
        {
            var connection = CreateSqliteConnection();
            connection.Open();
            return new TestDbContext(connection);
        }

        private static SQLiteConnection CreateSqliteConnection()
        {
            var connectionStringBuilder = new SQLiteConnectionStringBuilder
            {
                DataSource = ":memory:", 
                ForeignKeys = true
            };
            var connectionString = connectionStringBuilder.ConnectionString;
            return new SQLiteConnection(connectionString);
        }
    }
}
