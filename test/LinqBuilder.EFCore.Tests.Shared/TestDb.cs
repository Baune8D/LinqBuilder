using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore.Tests.Shared
{
    public class TestDb : IDisposable
    {
        private SqliteConnection _connection;

        public TestDb()
        {
            // We need to hold the SQLite connection open to force the in-memory database not to reset.
            _connection = CreateSqliteConnection();
            _connection.Open();

            // Create the context and ensure the schema is created.
            Context = CreateContext();
            Context.Database.EnsureCreated();
        }

        public TestDbContext Context { get; private set; }

        // Tear down in-memory database.
        public void Dispose()
        {
            Context?.Dispose();
            Context = null;

            _connection?.Dispose();
            _connection = null;
        }

        public void AddEntity(int value1, int value2, int? childValue = null)
        {
            var entity = new Entity
            {
                Value1 = value1,
                Value2 = value2
            };

            if (childValue.HasValue)
            {
                entity.ChildEntities = new List<ChildEntity>
                {
                    new ChildEntity
                    {
                        Value = childValue.Value
                    }
                };
            }

            Context.Entities.Add(entity);
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

        private TestDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseSqlite(_connection);
            return new TestDbContext(optionsBuilder.Options);
        }
    }
}
