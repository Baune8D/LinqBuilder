using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore.Tests.Data
{
    public sealed class TestDb : IDisposable
    {
        private readonly SqliteConnection _connection;

        public TestDb()
        {
            // We need to hold the SQLite connection open to force the in-memory database not to reset.
            _connection = CreateSqliteConnection();
            _connection.Open();

            // Create the context and ensure the schema is created.
            Context = CreateContext();
            Context.Database.EnsureCreated();
        }

        public TestDbContext Context { get; }

        // Tear down in-memory database.
        public void Dispose()
        {
            Context.Dispose();
            _connection.Dispose();
        }

        public void AddEntity(int value1, int value2, int? childValue = null)
        {
            var entity = new SomeEntity
            {
                Value1 = value1,
                Value2 = value2,
            };

            if (childValue.HasValue)
            {
                entity.ChildEntities = new List<SomeChildEntity>
                {
                    new SomeChildEntity
                    {
                        Value = childValue.Value,
                    },
                };
            }

            Context.Entities.Add(entity);
        }

        private static SqliteConnection CreateSqliteConnection()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = ":memory:",
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
