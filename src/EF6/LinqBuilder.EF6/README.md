# LinqBuilder.EF6

**Async Entity Framework 6 methods for LinqBuilder specifications.** This package adds `AnyAsync`, `AllAsync`, `CountAsync`, `FirstAsync`, `FirstOrDefaultAsync`, `SingleAsync` and `SingleOrDefaultAsync` overloads that take an `ISpecification<T>`. [LinqBuilder](https://www.nuget.org/packages/LinqBuilder) is an implementation of the specification pattern for LINQ.

```csharp
int adults = await db.People.CountAsync(new IsAdult());
List<Person> page = await db.People.ExeSpec(spec).ToListAsync();
```

Full documentation: [github.com/Baune8D/LinqBuilder](https://github.com/Baune8D/LinqBuilder#readme)

## Getting started

### 1. Install

```bash
dotnet add package LinqBuilder.EF6
```

This also installs `LinqBuilder`.

### 2. Write a specification

```csharp
using System;
using System.Linq.Expressions;
using LinqBuilder;

public class IsAdult : Specification<Person>
{
    public override Expression<Func<Person, bool>> AsExpression()
    {
        return person => person.Age >= 18;
    }
}
```

### 3. Query

```csharp
using System.Data.Entity;
using LinqBuilder;
using LinqBuilder.EF6;
using LinqBuilder.OrderBy;

var spec = new IsAdult()
    .OrderBy(OrderSpec<Person, string>.New(p => p.LastName))
    .Paginate(pageNo: 1, pageSize: 20);

List<Person> page = await db.People.ExeSpec(spec).ToListAsync(); // filtered, sorted and paged in SQL
int total = await db.People.CountAsync(spec);                     // filter only: all adults
bool anyAdults = await db.People.AnyAsync(new IsAdult());
```

`ExeSpec` returns an `IQueryable<T>`, so Entity Framework's own methods, such as `ToListAsync`, work on the result.

## Methods

Extension methods on `IQueryable<T>`, in the `LinqBuilder.EF6` namespace:

| Method | Returns |
| --- | --- |
| `AnyAsync(spec)` | `Task<bool>` |
| `AllAsync(spec)` | `Task<bool>` |
| `CountAsync(spec)` | `Task<int>` |
| `FirstAsync(spec)` | `Task<T>` |
| `FirstOrDefaultAsync(spec)` | `Task<T>`, with `null` if nothing matches |
| `SingleAsync(spec)` | `Task<T>` |
| `SingleOrDefaultAsync(spec)` | `Task<T>`, with `null` if nothing matches |

- They use only the filter of the specification and ignore its sort keys, `Skip` and `Take`.
- They have no `CancellationToken` parameter. To pass one, filter with `Where(spec)` and call Entity Framework's method: `await db.People.Where(spec).CountAsync(cancellationToken)`.
- For lists, and for results in the specification's sort order, use `ExeSpec` with Entity Framework's methods: `await db.People.ExeSpec(spec).ToListAsync(cancellationToken)`.

## Compatibility

The package targets .NET Standard 2.1 and depends on Entity Framework 6.5.2 or later. It runs on .NET Core 3.0 and later (including .NET 8, 9 and 10), but not on .NET Framework, which doesn't support .NET Standard 2.1.

## License

[MIT](https://github.com/Baune8D/LinqBuilder/blob/main/LICENSE.txt)
