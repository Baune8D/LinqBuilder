# LinqBuilder

[![Build](https://github.com/Baune8D/LinqBuilder/actions/workflows/pipeline.yml/badge.svg?branch=main)](https://github.com/Baune8D/LinqBuilder/actions/workflows/pipeline.yml)
[![codecov](https://codecov.io/gh/Baune8D/LinqBuilder/branch/main/graph/badge.svg)](https://codecov.io/gh/Baune8D/LinqBuilder)
[![License: MIT](https://img.shields.io/github/license/Baune8D/LinqBuilder)](https://github.com/Baune8D/LinqBuilder/blob/main/LICENSE.txt)

| Package | Version | Downloads |
| --- | --- | --- |
| LinqBuilder | [![NuGet Version](https://img.shields.io/nuget/v/LinqBuilder)](https://www.nuget.org/packages/LinqBuilder) | [![NuGet Downloads](https://img.shields.io/nuget/dt/LinqBuilder)](https://www.nuget.org/packages/LinqBuilder) |
| LinqBuilder.EFCore | [![NuGet Version](https://img.shields.io/nuget/v/LinqBuilder.EFCore)](https://www.nuget.org/packages/LinqBuilder.EFCore) | [![NuGet Downloads](https://img.shields.io/nuget/dt/LinqBuilder.EFCore)](https://www.nuget.org/packages/LinqBuilder.EFCore) |
| LinqBuilder.EF6 | [![NuGet Version](https://img.shields.io/nuget/v/LinqBuilder.EF6)](https://www.nuget.org/packages/LinqBuilder.EF6) | [![NuGet Downloads](https://img.shields.io/nuget/dt/LinqBuilder.EF6)](https://www.nuget.org/packages/LinqBuilder.EF6) |

**MyGet development feed:** [https://www.myget.org/F/baunegaard/api/v3/index.json](https://www.myget.org/F/baunegaard/api/v3/index.json)  

**The specification pattern for LINQ.** LinqBuilder lets you write a filter once, as a specification, combine it with other specifications, add ordering and paging, and run it on any `IQueryable<T>` or `IEnumerable<T>`. Specifications are expression trees, so Entity Framework Core, Entity Framework 6 and other LINQ providers translate them to SQL.

```csharp
var spec = new IsAdult()
    .And(new LastNameIs("Smith"))
    .OrderBy(OrderSpec<Person, string>.New(p => p.FirstName))
    .Paginate(pageNo: 1, pageSize: 20);

List<Person> page = await db.People.ExeSpec(spec).ToListAsync();
```

## Features

- **Reusable filters:** write a specification as a class (`Specification<T>`) or inline from a lambda (`Spec<T>.New(...)`).
- **Parameters:** `DynamicSpecification<T, TValue>` passes up to four values into the filter, e.g. `new LastNameIs("Smith")`.
- **Composition:** `And`, `Or` and `Not`, plus `Spec<T>.All(...)`, `Spec<T>.Any(...)` and `Spec<T>.None(...)`.
- **Ordering and paging:** `OrderBy`, `ThenBy`, `Skip`, `Take` and `Paginate` are stored in the specification and applied by `ExeSpec`.
- **`IQueryable<T>` and `IEnumerable<T>`:** a query gets the filter as an expression tree, an in-memory collection as a compiled delegate.
- **LINQ overloads:** `Where`, `Any`, `All`, `Count`, `First`, `FirstOrDefault`, `Single` and `SingleOrDefault` take a specification.
- **Async Entity Framework extensions:** `AnyAsync`, `AllAsync`, `CountAsync`, `FirstAsync`, `FirstOrDefaultAsync`, `SingleAsync` and `SingleOrDefaultAsync` for EF Core (`LinqBuilder.EFCore`) and EF6 (`LinqBuilder.EF6`).
- **In-memory checks:** `spec.IsSatisfiedBy(entity)`.
- **One specification for several entity types:** `MultiSpecification<T1, T2>`, up to four types.
- **Targets:** `LinqBuilder` .NET Standard 2.0 and .NET 8, 9 and 10. `LinqBuilder.EFCore` .NET 8, 9 and 10. `LinqBuilder.EF6` .NET Standard 2.1.

## Getting started

### 1. Install

```bash
dotnet add package LinqBuilder
```

For the async Entity Framework extensions, install `LinqBuilder.EFCore` (EF Core) or `LinqBuilder.EF6` (Entity Framework 6) instead. Both depend on `LinqBuilder`.

### 2. Write specifications

The examples use this entity:

```csharp
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Age { get; set; }
    public bool IsActive { get; set; }
}
```

A specification returns its filter from `AsExpression()`:

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

public class LastNameIs : DynamicSpecification<Person, string>
{
    public LastNameIs(string lastName)
        : base(lastName)
    {
    }

    public override Expression<Func<Person, bool>> AsExpression()
    {
        return person => person.LastName == Value;
    }
}
```

For a one-off filter you don't need a class: `Spec<Person>.New(p => p.IsActive)`.

### 3. Combine and run them

```csharp
using LinqBuilder;
using LinqBuilder.OrderBy;

ISpecification<Person> spec = new IsAdult()
    .And(new LastNameIs("Smith"))
    .OrderBy(OrderSpec<Person, string>.New(p => p.FirstName))
    .Paginate(pageNo: 2, pageSize: 10);

List<Person> page = people.ExeSpec(spec).ToList(); // filter, sort, skip 10, take 10
int total = people.Count(spec);                    // filter only: all adults named Smith
```

`people` can be any `IEnumerable<Person>` or `IQueryable<Person>`, such as a `List<Person>` or an EF Core `DbSet<Person>`.

### 4. Query asynchronously with Entity Framework

With `LinqBuilder.EFCore`:

```csharp
using LinqBuilder.EFCore;            // EF6: using LinqBuilder.EF6;
using Microsoft.EntityFrameworkCore; // EF6: using System.Data.Entity;

List<Person> page = await db.People.ExeSpec(spec).ToListAsync();
int total = await db.People.CountAsync(spec);
bool anyAdults = await db.People.AnyAsync(new IsAdult());
```

`ExeSpec` returns an `IQueryable<T>`, so EF Core's own methods, such as `ToListAsync`, work on the result.

## Specifications

Every specification implements `ISpecification<TEntity>`. `TEntity` must be a reference type.

| To create | Use |
| --- | --- |
| A reusable filter | A class that derives from `Specification<TEntity>` and overrides `AsExpression()` |
| A filter from a lambda | `Spec<TEntity>.New(expression)` or `new Spec<TEntity>(expression)` |
| A filter with parameters | A class that derives from `DynamicSpecification<TEntity, TValue>`, see [Dynamic specifications](#dynamic-specifications) |
| One filter for several entity types | A class that derives from `MultiSpecification<TEntity1, TEntity2>`, see [Multi-entity specifications](#multi-entity-specifications) |
| An empty specification | `Spec<TEntity>.New()`. It matches every entity. |

`Spec<TEntity>` is an alias of `Specification<TEntity>`: `Spec<Person>.New(...)` and `Specification<Person>.New(...)` do the same.

### Dynamic specifications

A dynamic specification passes one to four values into its filter:

| Base class | Values |
| --- | --- |
| `DynamicSpecification<TEntity, TValue>` | `Value` |
| `DynamicSpecification<TEntity, TValue1, TValue2>` | `Value1`, `Value2` |
| `DynamicSpecification<TEntity, TValue1, TValue2, TValue3>` | `Value1` to `Value3` |
| `DynamicSpecification<TEntity, TValue1, TValue2, TValue3, TValue4>` | `Value1` to `Value4` |

Set the values with the base constructor or with `Set(...)`:

```csharp
public class AgeBetween : DynamicSpecification<Person, int, int>
{
    public AgeBetween()
    {
    }

    public AgeBetween(int min, int max)
        : base(min, max)
    {
    }

    public override Expression<Func<Person, bool>> AsExpression()
    {
        return person => person.Age >= Value1 && person.Age <= Value2;
    }
}
```

```csharp
var twenties = new AgeBetween(20, 29);
var thirties = new AgeBetween().Set(30, 39);
```

`Set` changes the instance and returns it. The filter reads the values when it runs, so specifications already built from that instance (with `And`, `OrderBy` and so on) see the new values too. Use a new instance for each set of values.

### Multi-entity specifications

A multi-entity specification has a filter for each of two to four entity types:

```csharp
public class NameIsSmith : MultiSpecification<Person, Employee>
{
    public override Expression<Func<Person, bool>> AsExpressionForEntity1()
    {
        return person => person.LastName == "Smith";
    }

    public override Expression<Func<Employee, bool>> AsExpressionForEntity2()
    {
        return employee => employee.Surname == "Smith";
    }
}
```

```csharp
var nameIsSmith = new NameIsSmith();

var smiths = people.Where(nameIsSmith);                            // the first type is the default
var smithEmployees = employees.Where(nameIsSmith.For<Employee>());
```

`MultiSpecification<TEntity1, ...>` is an `ISpecification<TEntity1>`. `For<TEntity>()` returns the `Specification<TEntity>` for any of its types and throws `InvalidOperationException` for other types. `MultiSpecification<TEntity1, TEntity2, TEntity3>` and `MultiSpecification<TEntity1, TEntity2, TEntity3, TEntity4>` add `AsExpressionForEntity3()` and `AsExpressionForEntity4()`.

### Combining specifications

| Method | Matches |
| --- | --- |
| `a.And(b)` | Entities that match both `a` and `b` |
| `a.Or(b)` | Entities that match `a`, `b` or both |
| `a.Not()` | Entities that don't match `a` |
| `Spec<T>.All(a, b, c)` | Entities that match all of them, like `a.And(b).And(c)` |
| `Spec<T>.Any(a, b, c)` | Entities that match at least one of them, like `a.Or(b).Or(c)` |
| `Spec<T>.None(a, b, c)` | Entities that match none of them, like `a.Or(b).Or(c).Not()` |

- Each method returns a new specification. `a` and `b` are not changed.
- `And`, `Or` and `Not` keep the ordering, `Skip` and `Take` of `a`. Those of `b` are dropped. `All`, `Any` and `None` keep only the filters of their arguments.
- An empty specification is ignored by `And` and `Or`: `Spec<T>.New().And(a)` and `Spec<T>.New().Or(a)` both filter like `a`. `All()`, `Any()` and `None()` without arguments return an empty specification.

Start from an empty specification to build a filter step by step, e.g. from optional search parameters:

```csharp
ISpecification<Person> spec = Spec<Person>.New();

if (lastName != null)
{
    spec = spec.And(new LastNameIs(lastName));
}

if (minAge is int age)
{
    spec = spec.And(Spec<Person>.New(p => p.Age >= age));
}
```

## Ordering and paging

Ordering and paging are in the `LinqBuilder.OrderBy` namespace:

```csharp
using LinqBuilder.OrderBy;
```

### Order specifications

An order specification is one sort key. Create it from a lambda:

```csharp
IOrderSpecification<Person> byLastName = OrderSpec<Person, string>.New(p => p.LastName);
IOrderSpecification<Person> oldestFirst = OrderSpec<Person, int>.New(p => p.Age, Sort.Descending);
```

or as a class that derives from `OrderSpecification<TEntity, TResult>`:

```csharp
public class OldestFirst : OrderSpecification<Person, int>
{
    public OldestFirst()
        : base(Sort.Descending)
    {
    }

    public override Expression<Func<Person, int>> AsExpression()
    {
        return person => person.Age;
    }
}
```

The sort direction is `Sort.Ascending` (the default) or `Sort.Descending`. `OrderSpec<TEntity, TResult>` is an alias of `OrderSpecification<TEntity, TResult>`.

### Adding ordering to a specification

| Method | Returns | Description |
| --- | --- | --- |
| `spec.OrderBy(order)` | `IOrderedSpecification<T>` | Adds `order` as a sort key to `spec`. |
| `ordered.ThenBy(order)` | `IOrderedSpecification<T>` | Adds another sort key. Works on the result of `OrderBy` or `ThenBy`, and on an order specification. |
| `spec.UseOrdering(other)` | `ISpecification<T>` | Adds the sort keys of `other` to `spec`, and replaces the `Skip` and `Take` of `spec` with those of `other`. |

```csharp
// Adults, sorted by last name, then oldest first
ISpecification<Person> spec = new IsAdult()
    .OrderBy(byLastName)
    .ThenBy(new OldestFirst());

// The same ordering, defined once and attached to a specification
IOrderedSpecification<Person> ordering = byLastName.ThenBy(new OldestFirst());
ISpecification<Person> sortedAdults = new IsAdult().UseOrdering(ordering);
```

Use the return value of `UseOrdering`, and don't reuse `spec` afterwards. Unlike the other methods, `UseOrdering` changes `spec` in place if `spec` was itself built with methods such as `And`, `OrderBy` or `Skip`. Call `Clone()` first if you still need the original.

Order specifications also sort queries and collections directly:

```csharp
IOrderedEnumerable<Person> sorted = people.OrderBy(byLastName).ThenBy(new OldestFirst());
```

### Paging

| Method | Description |
| --- | --- |
| `spec.Skip(count)` | Skips the first `count` entities. |
| `spec.Take(count)` | Returns at most `count` entities. |
| `spec.Paginate(pageNo, pageSize)` | Returns page `pageNo`, starting at 1: `Skip((pageNo - 1) * pageSize)` and `Take(pageSize)`. Throws `ArgumentException` if `pageNo` or `pageSize` is less than 1. |

- `ExeSpec` applies `Skip` and `Take` after the filter and the sort keys.
- Calling `Skip` or `Take` again replaces the earlier value.
- These methods return `ISpecification<T>`, so add the ordering before the paging.

### Checking a specification

| Method | Returns `true` if |
| --- | --- |
| `spec.IsOrdered()` | `spec` has at least one sort key |
| `spec.HasSkip()` | `Skip` or `Paginate` was used |
| `spec.HasTake()` | `Take` or `Paginate` was used |

## Running specifications

### ExeSpec

`ExeSpec(spec)` applies the whole specification to an `IQueryable<T>` or `IEnumerable<T>`, in this order: filter, sort keys, `Skip`, `Take`. On an `IQueryable<T>` it returns an `IQueryable<T>`, so you can keep adding to the query.

```csharp
IQueryable<Person> query = db.People.ExeSpec(spec);
IEnumerable<Person> result = people.ExeSpec(spec);
```

`ExeSpec(spec, skipSort: true)` applies only the filter. The sort keys, `Skip` and `Take` are ignored.

### LINQ overloads

These methods take a specification on both `IQueryable<T>` and `IEnumerable<T>`:

| Method | Returns |
| --- | --- |
| `Where(spec)` | `IQueryable<T>` or `IEnumerable<T>` |
| `Any(spec)` | `bool` |
| `All(spec)` | `bool` |
| `Count(spec)` | `int` |
| `First(spec)` | `T` |
| `FirstOrDefault(spec)` | `T?` |
| `Single(spec)` | `T` |
| `SingleOrDefault(spec)` | `T?` |

They use only the filter and ignore the sort keys, `Skip` and `Take`. So `Count(spec)` on a paged specification returns the total number of matches, and `First(spec)` does not use the specification's sort order. For the first entity in that order, use `ExeSpec(spec).First()`.

### Checking one entity

```csharp
bool isAdult = new IsAdult().IsSatisfiedBy(person);
```

`IsSatisfiedBy` runs the filter on one entity. An empty specification is satisfied by every entity.

### Other methods

| Method | Description |
| --- | --- |
| `spec.Clone()` | Returns a new specification with the same filter, sort keys, `Skip` and `Take`. |
| `spec.Internal.QuerySpecification.AsExpression()` | Returns the filter as an `Expression<Func<T, bool>>`, or `null` for an empty specification. Use it to pass the filter to an API that takes an expression. |
| `spec.Internal.QuerySpecification.AsFunc()` | Returns the compiled filter as a `Func<T, bool>`, or `null` for an empty specification. |

## Entity Framework

### EF Core

`LinqBuilder.EFCore` adds async versions of the LINQ overloads to `IQueryable<T>`, in the `LinqBuilder.EFCore` namespace:

| Method | Returns |
| --- | --- |
| `AnyAsync(spec)` | `Task<bool>` |
| `AllAsync(spec)` | `Task<bool>` |
| `CountAsync(spec)` | `Task<int>` |
| `FirstAsync(spec)` | `Task<T>` |
| `FirstOrDefaultAsync(spec)` | `Task<T?>` |
| `SingleAsync(spec)` | `Task<T>` |
| `SingleOrDefaultAsync(spec)` | `Task<T?>` |

- Like the LINQ overloads, they use only the filter.
- They have no `CancellationToken` parameter. To pass one, filter with `Where(spec)` and call EF Core's method: `await db.People.Where(spec).CountAsync(cancellationToken)`.
- For lists, and for results in the specification's sort order, use `ExeSpec` with EF Core's methods: `await db.People.ExeSpec(spec).ToListAsync(cancellationToken)`.
- Each target framework uses the matching EF Core version: EF Core 8 on .NET 8, EF Core 9 on .NET 9 and EF Core 10 on .NET 10.

### Entity Framework 6

`LinqBuilder.EF6` has the same seven methods, in the `LinqBuilder.EF6` namespace. `FirstOrDefaultAsync` and `SingleOrDefaultAsync` return `Task<T>`, with `null` if nothing matches.

The package targets .NET Standard 2.1 and depends on Entity Framework 6.5.2 or later. It runs on .NET Core 3.0 and later (including .NET 8, 9 and 10), but not on .NET Framework, which doesn't support .NET Standard 2.1.

### Example: one page and the total count

```csharp
public record Page<T>(List<T> Items, int TotalCount);

public static async Task<Page<Person>> GetPageAsync(AppDbContext db, ISpecification<Person> filter, int pageNo, int pageSize)
{
    var spec = filter
        .OrderBy(OrderSpec<Person, int>.New(p => p.Id, Sort.Descending))
        .Paginate(pageNo, pageSize);

    List<Person> items = await db.People.ExeSpec(spec).ToListAsync();
    int totalCount = await db.People.CountAsync(spec); // ignores the ordering and paging

    return new Page<Person>(items, totalCount);
}
```

## Namespaces

| Namespace | Contains |
| --- | --- |
| `LinqBuilder` | `ISpecification<T>`, `Specification<T>`, `Spec<T>`, `DynamicSpecification<...>`, `MultiSpecification<...>`. `And`, `Or`, `Not`, `IsSatisfiedBy`, `Clone`, `ExeSpec` and the LINQ overloads. |
| `LinqBuilder.OrderBy` | `IOrderSpecification<T>`, `IOrderedSpecification<T>`, `OrderSpecification<T, TResult>`, `OrderSpec<T, TResult>`, `Sort`. `OrderBy`, `ThenBy`, `UseOrdering`, `Skip`, `Take`, `Paginate`, `IsOrdered`, `HasSkip` and `HasTake`. |
| `LinqBuilder.EFCore` | The async EF Core extensions (package `LinqBuilder.EFCore`). |
| `LinqBuilder.EF6` | The async EF6 extensions (package `LinqBuilder.EF6`). |
| `LinqBuilder.Internal` | `IQuerySpecification<T>` (the filter of a specification) and `SpecificationBase<T>`, used by the types above. |

## Related projects

- [LinqKit](https://github.com/scottksmith95/LinqKit): LinqBuilder uses its `PredicateBuilder` to combine filters with `And` and `Or`.

## Building from source

The build uses [NUKE](https://nuke.build/):

```bash
./build.sh Test        # Windows: .\build.cmd Test
```

## License

[MIT](https://github.com/Baune8D/LinqBuilder/blob/main/LICENSE.txt)
