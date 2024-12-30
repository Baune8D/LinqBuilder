using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LinqBuilder.EF6.Tests.Data;

public sealed class TestDb : IDisposable
{
    private readonly SQLiteConnection _connection;

    public TestDb()
    {
        _connection = CreateSqliteConnection();
        _connection.Open();
        Context = new TestDbContext(_connection);
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
                new()
                {
                    Value = childValue.Value,
                },
            };
        }

        Context.Entities.Add(entity);
    }

    private static SQLiteConnection CreateSqliteConnection()
    {
        var connectionStringBuilder = new SQLiteConnectionStringBuilder
        {
            DataSource = ":memory:",
            ForeignKeys = true,
        };
        var connectionString = connectionStringBuilder.ConnectionString;
        return new SQLiteConnection(connectionString);
    }
}
