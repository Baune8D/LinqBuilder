# LinqBuilder

**The specification pattern for LINQ.** LinqBuilder lets you write a filter once, as a specification, combine it with other specifications, add ordering and paging, and run it on any `IQueryable<T>` or `IEnumerable<T>`. Specifications are expression trees, so Entity Framework Core, Entity Framework 6 and other LINQ providers translate them to SQL.

```csharp
var spec = new IsAdult()
    .And(new LastNameIs("Smith"))
    .OrderBy(OrderSpec<Person, string>.New(p => p.FirstName))
    .Paginate(pageNo: 1, pageSize: 20);

List<Person> page = await db.People.ExeSpec(spec).ToListAsync();
```

Full documentation: [github.com/Baune8D/LinqBuilder](https://github.com/Baune8D/LinqBuilder#readme)

## Features

- **Reusable filters:** write a specification as a class (`Specification<T>`) or inline from a lambda (`Spec<T>.New(...)`).
- **Parameters:** `DynamicSpecification<T, TValue>` passes up to four values into the filter, e.g. `new LastNameIs("Smith")`.
- **Composition:** `And`, `Or` and `Not`, plus `Spec<T>.All(...)`, `Spec<T>.Any(...)` and `Spec<T>.None(...)`.
- **Ordering and paging:** `OrderBy`, `ThenBy`, `Skip`, `Take` and `Paginate` are stored in the specification and applied by `ExeSpec`.
- **`IQueryable<T>` and `IEnumerable<T>`:** a query gets the filter as an expression tree, an in-memory collection as a compiled delegate.
- **LINQ overloads:** `Where`, `Any`, `All`, `Count`, `First`, `FirstOrDefault`, `Single` and `SingleOrDefault` take a specification.
- **In-memory checks:** `spec.IsSatisfiedBy(entity)`.
- **One specification for several entity types:** `MultiSpecification<T1, T2>`, up to four types.
- **Targets .NET Standard 2.0 and .NET 8, 9 and 10.**

For async Entity Framework methods that take a specification (`AnyAsync`, `CountAsync`, `FirstOrDefaultAsync`, ...), install [LinqBuilder.EFCore](https://www.nuget.org/packages/LinqBuilder.EFCore) or [LinqBuilder.EF6](https://www.nuget.org/packages/LinqBuilder.EF6) instead. Both depend on this package.

## Getting started

### 1. Install

```bash
dotnet add package LinqBuilder
```

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

## Methods

### Creating and combining specifications

Namespace `LinqBuilder`.

| Method | Description |
| --- | --- |
| `Spec<T>.New(expression)` | A specification from a lambda. `Spec<T>` is an alias of `Specification<T>`. |
| `Spec<T>.New()` | An empty specification. It matches every entity and is ignored by `And` and `Or`. |
| `a.And(b)`, `a.Or(b)`, `a.Not()` | Combine filters. The result keeps the ordering, `Skip` and `Take` of `a`. |
| `Spec<T>.All(a, b, ...)` | Matches entities that match all of them. |
| `Spec<T>.Any(a, b, ...)` | Matches entities that match at least one of them. |
| `Spec<T>.None(a, b, ...)` | Same as `Spec<T>.Any(a, b, ...).Not()`: matches entities that match none of them. |

`All`, `Any` and `None` keep only the filters of their arguments. See [Specifications](https://github.com/Baune8D/LinqBuilder#specifications) for dynamic (`DynamicSpecification`) and multi-entity (`MultiSpecification`) specifications.

### Ordering and paging

Namespace `LinqBuilder.OrderBy`.

| Method | Description |
| --- | --- |
| `OrderSpec<T, TResult>.New(keySelector, sort)` | An order specification (one sort key). `sort` is `Sort.Ascending` (the default) or `Sort.Descending`. `OrderSpec` is an alias of `OrderSpecification`. |
| `spec.OrderBy(order)` | Adds `order` as a sort key. Returns an `IOrderedSpecification<T>`. |
| `ordered.ThenBy(order)` | Adds another sort key, on the result of `OrderBy` or `ThenBy`, or on an order specification. |
| `spec.UseOrdering(other)` | A copy of `spec` with the sort keys of `other` added. `Skip` and `Take` come from `other` when it sets them, otherwise from `spec`. |
| `spec.Skip(count)`, `spec.Take(count)` | Set the number of entities to skip and the maximum number to return. Calling them again replaces the value. |
| `spec.Paginate(pageNo, pageSize)` | `Skip((pageNo - 1) * pageSize)` and `Take(pageSize)`. `pageNo` starts at 1. |
| `spec.IsOrdered()`, `spec.HasSkip()`, `spec.HasTake()` | Check whether a specification has sort keys, a `Skip` or a `Take`. |

### Running specifications

Namespace `LinqBuilder`.

| Method | Description |
| --- | --- |
| `source.ExeSpec(spec)` | Applies the filter, sort keys, `Skip` and `Take`, in that order. Returns an `IQueryable<T>` for a query and an `IEnumerable<T>` for a collection. |
| `source.ExeSpec(spec, skipSort: true)` | Applies only the filter. |
| `Where`, `Any`, `All`, `Count`, `First`, `FirstOrDefault`, `Single`, `SingleOrDefault` | LINQ overloads that take a specification, on `IQueryable<T>` and `IEnumerable<T>`. They use only the filter and ignore the sort keys, `Skip` and `Take`. |
| `spec.IsSatisfiedBy(entity)` | Runs the filter on one entity. |
| `spec.Clone()` | A new specification with the same filter, sort keys, `Skip` and `Take`. |

## License

[MIT](https://github.com/Baune8D/LinqBuilder/blob/main/LICENSE)
