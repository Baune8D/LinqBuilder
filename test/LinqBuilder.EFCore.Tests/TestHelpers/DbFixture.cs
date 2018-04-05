using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore.Tests.TestHelpers
{
    public class DbFixture : IDisposable
    {
        private SqliteConnection _connection;
        public TestDbContext Context { get; private set; }

        public DbFixture()
        {
            // We need to hold the SQLite connection open to force the in-memory database not to reset.
            _connection = CreateSqliteConnection();
            _connection.Open();

            // Create the context and ensure the schema is created.
            Context = CreateContext();
            Context.Database.EnsureCreated();
        }

        // Tear down in-memory database.
        public void Dispose()
        {
            Context?.Dispose();
            Context = null;

            _connection?.Dispose();
            _connection = null;
        }

        private TestDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseSqlite(_connection);
            return new TestDbContext(optionsBuilder.Options);
        }

        private static SqliteConnection CreateSqliteConnection()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = ":memory:"
            };
            var connectionString = connectionStringBuilder.ToString();
            return new SqliteConnection(connectionString);
        }
    }
}
